using Firepuma.EskomLoadShedding.FunctionApp.Schedules;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.Tests.FunctionApp.Schedules.Models.ValueObjects;

public class GetOfflineSlotsForAreaTests
{
    [Fact]
    public async Task GetOfflineSlots_Stage_1_Area_3_Date_20220616_should_have_1_slot()
    {
        // Arrange
        const int stage = 1;
        const int area = 3;
        var schedule = await new RawSchedulesParser().ParseScheduleAsync(stage);
        var date = new DateTime(2022, 6, 16);

        // Act
        var timeSlots = schedule.GetOfflineSlotsForAreaOnDate(area, date);

        // Assert
        Assert.Single(timeSlots);
        AssertSlotEquals(22, 0, 0, 30, timeSlots[0]);
    }
    
    [Fact]
    public async Task GetOfflineSlots_Stage_6_Area_3_Date_20220702_should_have_4_slots()
    {
        // Arrange
        const int stage = 6;
        const int area = 3;
        var schedule = await new RawSchedulesParser().ParseScheduleAsync(stage);
        var date = new DateTime(2022, 7, 2);

        // Act
        var timeSlots = schedule.GetOfflineSlotsForAreaOnDate(area, date);

        // Assert
        Assert.Equal(4, timeSlots.Length);
        AssertSlotEquals(4, 0, 6, 30, timeSlots[0]);
        AssertSlotEquals(10, 0, 12, 30, timeSlots[1]);
        AssertSlotEquals(12, 0, 14, 30, timeSlots[2]);
        AssertSlotEquals(20, 0, 22, 30, timeSlots[3]);
    }
    
    [Fact]
    public async Task GetOfflineSlots_Stage_8_Area_3_Date_20220610_should_have_6_slots()
    {
        // Arrange
        const int stage = 8;
        const int area = 3;
        var schedule = await new RawSchedulesParser().ParseScheduleAsync(stage);
        var date = new DateTime(2022, 6, 10);

        // Act
        var timeSlots = schedule.GetOfflineSlotsForAreaOnDate(area, date);

        // Assert
        Assert.Equal(6, timeSlots.Length);
        AssertSlotEquals(0, 0, 2, 30, timeSlots[0]);
        AssertSlotEquals(6, 0, 8, 30, timeSlots[1]);
        AssertSlotEquals(8, 0, 10, 30, timeSlots[2]);
        AssertSlotEquals(14, 0, 16, 30, timeSlots[3]);
        AssertSlotEquals(16, 0, 18, 30, timeSlots[4]);
        AssertSlotEquals(22, 0, 0, 30, timeSlots[5]);
    }

    private void AssertSlotEquals(
        int expectedStartHour,
        int expectedStartMinute,
        int expectedEndHour,
        int expectedEndMinute,
        LoadSheddingSchedule.TimeRange slot)
    {
        Assert.Equal(
            new LoadSheddingSchedule.TimeRange(
                new TimeSpan(expectedStartHour, expectedStartMinute, 0),
                new TimeSpan(expectedEndHour, expectedEndMinute, 0)), 
            slot);
    }
}