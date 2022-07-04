using System;
using System.Runtime.Serialization;

namespace Firepuma.EskomLoadShedding.FunctionApp.Schedules.Exceptions;

[Serializable]
public class UnableToParseScheduleException : Exception
{
    public UnableToParseScheduleException()
    {
    }

    public UnableToParseScheduleException(string message)
        : base(message)
    {
    }

    public UnableToParseScheduleException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected UnableToParseScheduleException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }
}