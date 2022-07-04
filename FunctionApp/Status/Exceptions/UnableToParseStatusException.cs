using System;
using System.Runtime.Serialization;

namespace Firepuma.EskomLoadShedding.FunctionApp.Status.Exceptions;

[Serializable]
public class UnableToParseStatusException : Exception
{
    public UnableToParseStatusException()
    {
    }

    public UnableToParseStatusException(string message)
        : base(message)
    {
    }

    public UnableToParseStatusException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected UnableToParseStatusException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}