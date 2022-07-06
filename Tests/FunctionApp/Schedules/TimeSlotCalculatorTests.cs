using Firepuma.EskomLoadShedding.FunctionApp.Schedules;
using Firepuma.EskomLoadShedding.FunctionApp.Schedules.Models.ValueObjects;

namespace Firepuma.EskomLoadShedding.Tests.FunctionApp.Schedules;

public class TimeSlotCalculatorTests
{
    [Fact]
    public void CalculateDateTimes_start_of_day_results_in_same_day_dates()
    {
        // Arrange
        var date = new DateTime(2022, 7, 13, 0, 0, 0, DateTimeKind.Utc);
        var timeSlot = new LoadSheddingSchedule.TimeRange(
            new TimeSpan(0, 0, 0),
            new TimeSpan(2, 30, 0));

        var calculator = new TimeSlotCalculator();

        // Act
        calculator.CalculateDateTimes(date, timeSlot, out var startTime, out var endTime);

        // Assert
        Assert.Equal(new DateTime(2022, 7, 13, 0, 0, 0, DateTimeKind.Utc), startTime);
        Assert.Equal(new DateTime(2022, 7, 13, 2, 30, 0, DateTimeKind.Utc), endTime);
    }
    
    [Fact]
    public void CalculateDateTimes_middle_of_day_results_in_same_day_dates()
    {
        // Arrange
        var date = new DateTime(2022, 7, 13, 0, 0, 0, DateTimeKind.Utc);
        var timeSlot = new LoadSheddingSchedule.TimeRange(
            new TimeSpan(14, 0, 0),
            new TimeSpan(16, 30, 0));

        var calculator = new TimeSlotCalculator();

        // Act
        calculator.CalculateDateTimes(date, timeSlot, out var startTime, out var endTime);

        // Assert
        Assert.Equal(new DateTime(2022, 7, 13, 14, 0, 0, DateTimeKind.Utc), startTime);
        Assert.Equal(new DateTime(2022, 7, 13, 16, 30, 0, DateTimeKind.Utc), endTime);
    }
    
    [Fact]
    public void CalculateDateTimes_end_of_day_results_in_end_date_becoming_next_day()
    {
        // Arrange
        var date = new DateTime(2022, 7, 13, 0, 0, 0, DateTimeKind.Utc);
        var timeSlot = new LoadSheddingSchedule.TimeRange(
            new TimeSpan(22, 0, 0),
            new TimeSpan(0, 30, 0));

        var calculator = new TimeSlotCalculator();

        // Act
        calculator.CalculateDateTimes(date, timeSlot, out var startTime, out var endTime);

        // Assert
        Assert.Equal(new DateTime(2022, 7, 13, 22, 0, 0, DateTimeKind.Utc), startTime);
        Assert.Equal(new DateTime(2022, 7, 14, 0, 30, 0, DateTimeKind.Utc), endTime);
    }
}