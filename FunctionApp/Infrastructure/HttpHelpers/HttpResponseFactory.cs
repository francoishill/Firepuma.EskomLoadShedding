using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.HttpHelpers;

public static class HttpResponseFactory
{
    public static IActionResult CreateBadRequestResponse(params string[] errors)
    {
        return new BadRequestObjectResult(new Dictionary<string, object>
        {
            ["Errors"] = errors,
        });
    }
}