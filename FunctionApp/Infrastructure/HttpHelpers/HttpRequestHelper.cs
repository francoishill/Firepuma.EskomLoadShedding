using System;
using Microsoft.AspNetCore.Http;

namespace Firepuma.EskomLoadShedding.FunctionApp.Infrastructure.HttpHelpers;

public static class HttpRequestHelper
{
    public static bool TryGetRequiredEnumQueryParam<TEnum>(
        this HttpRequest req,
        string paramName,
        out TEnum paramValue,
        out string validationError)
        where TEnum : struct
    {
        if (string.IsNullOrWhiteSpace(req.Query[paramName]))
        {
            paramValue = default;
            validationError = $"Query param {paramName} is empty but required";
            return false;
        }

        if (!Enum.TryParse(req.Query[paramName], true, out paramValue))
        {
            paramValue = default;
            validationError = $"Query param {paramName} should be a valid enum '{typeof(TEnum).Name}' but '{req.Query[paramName]}' is invalid";
            return false;
        }

        validationError = null;
        return true;
    }

    public static bool TryGetRequiredIntQueryParam(
        this HttpRequest req,
        string paramName,
        out int paramValue,
        out string validationError)
    {
        if (string.IsNullOrWhiteSpace(req.Query[paramName]))
        {
            paramValue = -2;
            validationError = $"Query param {paramName} is empty but required";
            return false;
        }

        if (!int.TryParse(req.Query[paramName], out paramValue))
        {
            paramValue = -2;
            validationError = $"Query param {paramName} should be a number but '{req.Query[paramName]}' is not a number";
            return false;
        }

        validationError = null;
        return true;
    }

    public static bool TryGetRequiredDateQueryParam(
        this HttpRequest req,
        string paramName,
        out DateTime paramValue,
        out string validationError)
    {
        if (string.IsNullOrWhiteSpace(req.Query[paramName]))
        {
            paramValue = DateTime.MinValue;
            validationError = $"Query param {paramName} is empty but required";
            return false;
        }

        if (!DateTime.TryParse(req.Query[paramName], out paramValue))
        {
            paramValue = DateTime.MinValue;
            validationError = $"Query param {paramName} should be a number but '{req.Query[paramName]}' is not a number";
            return false;
        }

        validationError = null;
        return true;
    }
}