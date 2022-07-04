using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.Tests.FunctionApp.RawScheduleTables;

public static class RawScheduleTestsHelper
{
    public static int[] GetAreasInDaySlot(
        this LoadSheddingSchedule schedule,
        int dayOfMonth,
        int startHour,
        int startMinute,
        int endHour,
        int endMinute)
    {
        return schedule
            .DayOfMonthAndSlotsMappings[dayOfMonth]
            .SlotsAndAreasMap[new LoadSheddingSchedule.TimeRange(
                new TimeSpan(startHour, startMinute, 0),
                new TimeSpan(endHour, endMinute, 0))]
            .ToArray();
    }
}