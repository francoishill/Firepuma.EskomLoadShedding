using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable NotAccessedPositionalProperty.Global

namespace Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

public class LoadSheddingSchedule
{
    public Dictionary<int, SlotsAndAreas> DayOfMonthAndSlotsMappings { get; set; } = new();

    public class SlotsAndAreas
    {
        public Dictionary<TimeRange, List<int>> SlotsAndAreasMap { get; set; } = new();
    }

    public record TimeRange(TimeSpan Start, TimeSpan End);

    public TimeRange[] GetOfflineSlotsForAreaOnDate(
        int areaNumber,
        DateTime date)
    {
        var dayOfMonth = date.Day;
        return DayOfMonthAndSlotsMappings[dayOfMonth]
            .SlotsAndAreasMap
            .Where(pair => pair.Value.Contains(areaNumber))
            .Select(pair => pair.Key)
            .ToArray();
    }
}