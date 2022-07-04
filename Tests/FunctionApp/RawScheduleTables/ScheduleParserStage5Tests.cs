﻿using Firepuma.EskomLoadShedding.FunctionApp.Schedules;

namespace Firepuma.EskomLoadShedding.Tests.FunctionApp.RawScheduleTables;

public class ScheduleParserStage5Tests
{
    private const int STAGE_NUMBER = 5;

    [Fact]
    public async Task ParseSchedule_0000_to_0230_areas_are_correct()
    {
        // Arrange
        var parser = new RawSchedulesParser();

        // Act
        var schedule = await parser.ParseScheduleAsync(STAGE_NUMBER);

        // Assert
        Assert.NotNull(schedule);
        Assert.Equal(31, schedule.DayOfMonthAndSlotsMappings.Count);

        Assert.Equal(new[] { 1, 9, 13, 5, 2 }, schedule.GetAreasInDaySlot(1, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 14 }, schedule.GetAreasInDaySlot(2, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 10 }, schedule.GetAreasInDaySlot(3, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 6 }, schedule.GetAreasInDaySlot(4, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 3 }, schedule.GetAreasInDaySlot(5, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 15 }, schedule.GetAreasInDaySlot(6, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 11 }, schedule.GetAreasInDaySlot(7, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 7 }, schedule.GetAreasInDaySlot(8, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 4 }, schedule.GetAreasInDaySlot(9, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 16 }, schedule.GetAreasInDaySlot(10, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 12 }, schedule.GetAreasInDaySlot(11, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 8 }, schedule.GetAreasInDaySlot(12, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 1 }, schedule.GetAreasInDaySlot(13, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 13 }, schedule.GetAreasInDaySlot(14, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 9 }, schedule.GetAreasInDaySlot(15, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 5 }, schedule.GetAreasInDaySlot(16, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 2 }, schedule.GetAreasInDaySlot(17, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 14 }, schedule.GetAreasInDaySlot(18, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 10 }, schedule.GetAreasInDaySlot(19, 0, 0, 2, 30));
        Assert.Equal(new[] { 1, 9, 13, 5, 6 }, schedule.GetAreasInDaySlot(20, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 3 }, schedule.GetAreasInDaySlot(21, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 15 }, schedule.GetAreasInDaySlot(22, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 11 }, schedule.GetAreasInDaySlot(23, 0, 0, 2, 30));
        Assert.Equal(new[] { 2, 10, 14, 6, 7 }, schedule.GetAreasInDaySlot(24, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 4 }, schedule.GetAreasInDaySlot(25, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 16 }, schedule.GetAreasInDaySlot(26, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 12 }, schedule.GetAreasInDaySlot(27, 0, 0, 2, 30));
        Assert.Equal(new[] { 3, 11, 15, 7, 8 }, schedule.GetAreasInDaySlot(28, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 1 }, schedule.GetAreasInDaySlot(29, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 13 }, schedule.GetAreasInDaySlot(30, 0, 0, 2, 30));
        Assert.Equal(new[] { 4, 12, 16, 8, 9 }, schedule.GetAreasInDaySlot(31, 0, 0, 2, 30));
    }

    [Fact]
    public async Task ParseSchedule_1400_to_1630_areas_are_correct()
    {
        // Arrange
        var parser = new RawSchedulesParser();

        // Act
        var schedule = await parser.ParseScheduleAsync(STAGE_NUMBER);

        // Assert
        Assert.NotNull(schedule);
        Assert.Equal(31, schedule.DayOfMonthAndSlotsMappings.Count);

        Assert.Equal(new[] { 8, 16, 4, 12, 9 }, schedule.GetAreasInDaySlot(1, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 5 }, schedule.GetAreasInDaySlot(2, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 1 }, schedule.GetAreasInDaySlot(3, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 13 }, schedule.GetAreasInDaySlot(4, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 10 }, schedule.GetAreasInDaySlot(5, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 6 }, schedule.GetAreasInDaySlot(6, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 2 }, schedule.GetAreasInDaySlot(7, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 14 }, schedule.GetAreasInDaySlot(8, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 11 }, schedule.GetAreasInDaySlot(9, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 7 }, schedule.GetAreasInDaySlot(10, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 3 }, schedule.GetAreasInDaySlot(11, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 15 }, schedule.GetAreasInDaySlot(12, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 8 }, schedule.GetAreasInDaySlot(13, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 4 }, schedule.GetAreasInDaySlot(14, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 16 }, schedule.GetAreasInDaySlot(15, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 12 }, schedule.GetAreasInDaySlot(16, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 9 }, schedule.GetAreasInDaySlot(17, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 5 }, schedule.GetAreasInDaySlot(18, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 1 }, schedule.GetAreasInDaySlot(19, 14, 0, 16, 30));
        Assert.Equal(new[] { 8, 16, 4, 12, 13 }, schedule.GetAreasInDaySlot(20, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 10 }, schedule.GetAreasInDaySlot(21, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 6 }, schedule.GetAreasInDaySlot(22, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 2 }, schedule.GetAreasInDaySlot(23, 14, 0, 16, 30));
        Assert.Equal(new[] { 9, 1, 5, 13, 14 }, schedule.GetAreasInDaySlot(24, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 11 }, schedule.GetAreasInDaySlot(25, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 7 }, schedule.GetAreasInDaySlot(26, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 3 }, schedule.GetAreasInDaySlot(27, 14, 0, 16, 30));
        Assert.Equal(new[] { 10, 2, 6, 14, 15 }, schedule.GetAreasInDaySlot(28, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 8 }, schedule.GetAreasInDaySlot(29, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 4 }, schedule.GetAreasInDaySlot(30, 14, 0, 16, 30));
        Assert.Equal(new[] { 11, 3, 7, 15, 16 }, schedule.GetAreasInDaySlot(31, 14, 0, 16, 30));
    }

    [Fact]
    public async Task ParseSchedule_2200_to_0030_areas_are_correct()
    {
        // Arrange
        var parser = new RawSchedulesParser();

        // Act
        var schedule = await parser.ParseScheduleAsync(STAGE_NUMBER);

        // Assert
        Assert.NotNull(schedule);
        Assert.Equal(31, schedule.DayOfMonthAndSlotsMappings.Count);

        Assert.Equal(new[] { 12, 4, 8, 16, 13 }, schedule.GetAreasInDaySlot(1, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 9 }, schedule.GetAreasInDaySlot(2, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 5 }, schedule.GetAreasInDaySlot(3, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 1 }, schedule.GetAreasInDaySlot(4, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 14 }, schedule.GetAreasInDaySlot(5, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 10 }, schedule.GetAreasInDaySlot(6, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 6 }, schedule.GetAreasInDaySlot(7, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 2 }, schedule.GetAreasInDaySlot(8, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 15 }, schedule.GetAreasInDaySlot(9, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 11 }, schedule.GetAreasInDaySlot(10, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 7 }, schedule.GetAreasInDaySlot(11, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 3 }, schedule.GetAreasInDaySlot(12, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 12 }, schedule.GetAreasInDaySlot(13, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 8 }, schedule.GetAreasInDaySlot(14, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 4 }, schedule.GetAreasInDaySlot(15, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 16 }, schedule.GetAreasInDaySlot(16, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 13 }, schedule.GetAreasInDaySlot(17, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 9 }, schedule.GetAreasInDaySlot(18, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 5 }, schedule.GetAreasInDaySlot(19, 22, 0, 0, 30));
        Assert.Equal(new[] { 12, 4, 8, 16, 1 }, schedule.GetAreasInDaySlot(20, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 14 }, schedule.GetAreasInDaySlot(21, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 10 }, schedule.GetAreasInDaySlot(22, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 6 }, schedule.GetAreasInDaySlot(23, 22, 0, 0, 30));
        Assert.Equal(new[] { 13, 5, 9, 1, 2 }, schedule.GetAreasInDaySlot(24, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 15 }, schedule.GetAreasInDaySlot(25, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 11 }, schedule.GetAreasInDaySlot(26, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 7 }, schedule.GetAreasInDaySlot(27, 22, 0, 0, 30));
        Assert.Equal(new[] { 14, 6, 10, 2, 3 }, schedule.GetAreasInDaySlot(28, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 12 }, schedule.GetAreasInDaySlot(29, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 8 }, schedule.GetAreasInDaySlot(30, 22, 0, 0, 30));
        Assert.Equal(new[] { 15, 7, 11, 3, 4 }, schedule.GetAreasInDaySlot(31, 22, 0, 0, 30));
    }
}