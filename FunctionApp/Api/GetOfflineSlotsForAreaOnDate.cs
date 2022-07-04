using System.Threading.Tasks;
using Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.HttpHelpers;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Firepuma.EskomLoadShedding.FunctionApp.Api;

public static class GetOfflineSlotsForAreaOnDate
{
    [FunctionName("GetOfflineSlotsForAreaOnDate")]
    public static async Task<IActionResult> RunAsync(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request");

        if (!req.TryGetRequiredIntQueryParam("stage", out var stage, out var stageValidationError))
        {
            return HttpResponseFactory.CreateBadRequestResponse(stageValidationError);
        }

        if (!req.TryGetRequiredIntQueryParam("areaNumber", out var areaNumber, out var areaValidationError))
        {
            return HttpResponseFactory.CreateBadRequestResponse(areaValidationError);
        }

        if (!req.TryGetRequiredDateQueryParam("date", out var date, out var dateValidationError))
        {
            return HttpResponseFactory.CreateBadRequestResponse(dateValidationError);
        }

        var parser = new RawSchedulesParser();
        var schedule = await parser.ParseScheduleAsync(stage);

        var timeSlots = schedule.GetOfflineSlotsForAreaOnDate(areaNumber, date);
        return new OkObjectResult(timeSlots);
    }
}