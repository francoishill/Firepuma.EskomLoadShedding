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

public class GetLoadsheddingSlots
{
    private readonly RawSchedulesParser _parser;

    public GetLoadsheddingSlots(
        RawSchedulesParser parser)
    {
        _parser = parser;
    }

    [FunctionName("GetLoadsheddingSlots")]
    public async Task<IActionResult> RunAsync(
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

        var schedule = await _parser.ParseScheduleAsync(stage);

        var timeSlots = schedule.GetOfflineSlotsForAreaOnDate(areaNumber, date);
        return new OkObjectResult(timeSlots);
    }
}