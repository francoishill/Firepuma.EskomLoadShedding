using Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.HttpHelpers;

public class OkVCalendarObjectResult : OkObjectResult
{
    public OkVCalendarObjectResult(object value)
        : base(value)
    {
        Formatters = new FormatterCollection<IOutputFormatter>
        {
            new VCalendarOutputFormatter(),
        };
    }
}