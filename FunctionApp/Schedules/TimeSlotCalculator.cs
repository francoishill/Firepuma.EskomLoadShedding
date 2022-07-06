using System;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.FunctionApp.Schedules;

public class TimeSlotCalculator
{
    public void CalculateDateTimes(
        DateTime date,
        LoadSheddingSchedule.TimeRange timeSlot,
        out DateTime startTime,
        out DateTime endTime)
    {
        startTime = date.Add(timeSlot.Start);

        endTime = date.Add(timeSlot.End.Hours == 0
            ? timeSlot.End.Add(TimeSpan.FromHours(24))
            : timeSlot.End);
    }
}