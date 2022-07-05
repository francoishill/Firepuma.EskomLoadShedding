using Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.HttpHelpers;

public class OkVCalendarObjectResult : OkObjectResult
{
    public OkVCalendarObjectResult(object value)
        : base(value)
    {
        // Thanks to https://github.com/Azure/azure-functions-host/issues/6490
        Formatters = new FormatterCollection<IOutputFormatter>
        {
            new VCalendarOutputFormatter(),
        };
    }
}