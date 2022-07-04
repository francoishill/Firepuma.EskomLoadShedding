using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.HttpHelpers;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Firepuma.EskomLoadShedding.FunctionApp.Api;

public static class GetLoadsheddingCalendar
{
    [FunctionName("GetLoadsheddingCalendar")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request");

        if (!req.TryGetRequiredEnumQueryParam("customerType", out CustomerType customerType, out var customerTypeValidationError))
        {
            return HttpResponseFactory.CreateBadRequestResponse(customerTypeValidationError);
        }

        if (customerType != CustomerType.CityOfCapeTown)
        {
            //TODO: add support for Eskom customers too
            return HttpResponseFactory.CreateBadRequestResponse("Currently only CityOfCapeTown customerType is supported");
        }

        if (!req.TryGetRequiredIntQueryParam("areaNumber", out var areaNumber, out var areaValidationError))
        {
            return HttpResponseFactory.CreateBadRequestResponse(areaValidationError);
        }

        var events = new List<VCalendar.CalendarEvent>();

        var startOfToday = DateTime.Now.Date;

        var parser = new RawSchedulesParser();

        for (var daysAgo = -3; daysAgo <= 14; daysAgo++)
        {
            var date = startOfToday.AddDays(daysAgo);

            var timeSlotsWithMinimumStage = new Dictionary<LoadSheddingSchedule.TimeRange, int>();

            for (var stage = 1; stage <= 8; stage++)
            {
                var schedule = await parser.ParseScheduleAsync(stage);

                var timeSlots = schedule.GetOfflineSlotsForAreaOnDate(areaNumber, date);
                foreach (var timeSlot in timeSlots)
                {
                    if (!timeSlotsWithMinimumStage.ContainsKey(timeSlot))
                    {
                        timeSlotsWithMinimumStage.Add(timeSlot, stage);
                    }
                }
            }

            foreach (var (timeSlot, minimumStage) in timeSlotsWithMinimumStage.OrderBy(pair => pair.Key.Start))
            {
                var eventTitle = $"Stage {minimumStage}+ {timeSlot.Start.ToString()} - {timeSlot.End.ToString()}";

                var categories = new List<string>();
                for (var i = minimumStage; i <= 8; i++)
                {
                    categories.Add($"Stage {i}");
                }

                events.Add(new VCalendar.CalendarEvent
                {
                    Organizer = new VCalendar.CalendarEvent.Member("", ""),
                    Attendees = Array.Empty<VCalendar.CalendarEvent.Member>(),
                    Uid = Guid.NewGuid().ToString(),
                    Summary = eventTitle,
                    Categories = categories,
                    Start = date.Add(timeSlot.Start),
                    End = date.Add(timeSlot.End),
                    Created = new DateTime(2022, 7, 2),
                    Updated = new DateTime(2022, 7, 2),
                });
            }
        }

        var calendar = new VCalendar
        {
            CalendarName = customerType.ToString(),
            CalendarEvents = events,
        };

        return new OkVCalendarObjectResult(calendar);
    }
}