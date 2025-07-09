using Microsoft.Extensions.Time.Testing;
using Moq;

namespace HouseControl.Library.Tests;

public class ScheduleHelperTests
{
    private TimeSpan timeZoneOffset = new(-2, 0, 0);
    private DateTimeOffset today;
    private DateTimeOffset mondayInPast;
    private DateTimeOffset mondayInFuture;
    private DateTimeOffset sundayInPast;
    private DateTimeOffset sundayInFuture;

    private ScheduleHelper GetScheduleHelperForThursday()
    {
        today = new(2025, 06, 05, 15, 52, 00, timeZoneOffset);
        mondayInPast = today.AddDays(-3);
        mondayInFuture = today.AddDays(4);
        sundayInPast = today.AddDays(-4);
        sundayInFuture = today.AddDays(3);

        ScheduleHelper helper = new(new FakeSunsetProvider());
        helper.HelperTimeProvider = new FakeTimeProvider(today);
        return helper;
    }

    private ScheduleHelper GetScheduleHelperForFriday()
    {
        today = new(2025, 06, 06, 15, 52, 00, timeZoneOffset);
        mondayInPast = today.AddDays(-4);
        mondayInFuture = today.AddDays(3);
        sundayInPast = today.AddDays(-5);
        sundayInFuture = today.AddDays(2);

        ScheduleHelper helper = new(new FakeSunsetProvider());
        helper.HelperTimeProvider = new FakeTimeProvider(today);
        return helper;
    }

    [Fact]
    public void MondayItemInPast_OnRollDay_IsTomorrow()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();
        DateTimeOffset expected =
            new(today.Date.AddDays(1) + mondayInPast.TimeOfDay,
                timeZoneOffset);
        ScheduleInfo info = new()
        {
            EventTime = mondayInPast,
            TimeType = ScheduleTimeType.Standard,
        };

        // Act
        DateTimeOffset actual = helper.RollForwardToNextDay(info);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MondayItemInFuture_OnRollDay_IsUnchanged()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();
        DateTimeOffset expected = mondayInFuture;
        ScheduleInfo info = new()
        {
            EventTime = mondayInFuture,
            TimeType = ScheduleTimeType.Standard,
        };

        // Act
        DateTimeOffset actual = helper.RollForwardToNextDay(info);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MondayItemInPastAndTodayFriday_OnRollWeekdayDay_IsNextMonday()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForFriday();
        DateTimeOffset expected =
            new(today.Date.AddDays(3) + mondayInPast.TimeOfDay,
                timeZoneOffset);
        ScheduleInfo info = new()
        {
            EventTime = mondayInPast,
            TimeType = ScheduleTimeType.Standard,
        };

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekdayDay(info);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MondayItemInFutureAndTodayFriday_OnRollWeekdayDay_IsUnchanged()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForFriday();
        DateTimeOffset expected = mondayInFuture;
        ScheduleInfo info = new()
        {
            EventTime = mondayInFuture,
            TimeType = ScheduleTimeType.Standard,
        };

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekdayDay(info);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SundayItemInPastAndTodayFriday_OnRollWeekendDay_IsSaturday()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForFriday();
        DateTimeOffset expected =
            new(today.Date.AddDays(1) + sundayInPast.TimeOfDay,
                timeZoneOffset);
        ScheduleInfo info = new()
        {
            EventTime = sundayInPast,
            TimeType = ScheduleTimeType.Standard,
        };

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekendDay(info);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void SundayItemInFutureAndTodayFriday_OnRollWeekendDay_IsUnchanged()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForFriday();
        DateTimeOffset expected = sundayInFuture;
        ScheduleInfo info = new()
        {
            EventTime = sundayInFuture,
            TimeType = ScheduleTimeType.Standard,
        };

        // Act
        DateTimeOffset actual = helper.RollForwardToNextWeekendDay(info);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Now_ReturnsConfiguredTime()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();
        DateTimeOffset expected = today;

        // Act
        var actual = helper.Now();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Tomorrow_ReturnsConfiguredDatePlusOne()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();
        DateTimeOffset expected = new DateTimeOffset(today.Date.AddDays(1), today.Offset);

        // Act
        var actual = helper.Tomorrow();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Today_ReturnsConfiguredDate()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();
        DateTimeOffset expected = new DateTimeOffset(today.Date, today.Offset);

        // Act
        var actual = helper.Today();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void IsInFuture_WithPastDate_ReturnsFalse()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();

        // Act
        var actual = helper.IsInFuture(mondayInPast);

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void IsInFuture_WithFutureDate_ReturnsTrue()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();

        // Act
        var actual = helper.IsInFuture(mondayInFuture);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void IsInPast_WithPastDate_ReturnsTrue()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();

        // Act
        var actual = helper.IsInPast(mondayInPast);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void IsInPast_WithFutureDate_ReturnsFalse()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();

        // Act
        var actual = helper.IsInPast(mondayInFuture);

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void DurationFromNow_Time10MinutesInPast_Returns10Minutes()
    {
        // Arrange
        ScheduleHelper helper = GetScheduleHelperForThursday();
        DateTimeOffset testTime = today.AddMinutes(-10);
        TimeSpan expected = new(0, 10, 0);

        // Act
        var actual = helper.DurationFromNow(testTime);

        // Assert
        Assert.Equal(expected, actual);
    }
}