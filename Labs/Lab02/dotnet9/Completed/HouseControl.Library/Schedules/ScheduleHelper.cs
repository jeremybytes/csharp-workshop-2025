﻿using HouseControl.Sunset;

namespace HouseControl.Library;

public class ScheduleHelper
{
    private readonly ISunsetProvider SunsetProvider;

    private TimeProvider? helperTimeProvider;
    public TimeProvider HelperTimeProvider
    {
        get => helperTimeProvider ??= TimeProvider.System; 
        set => helperTimeProvider = value;
    }

    public ScheduleHelper(ISunsetProvider sunsetProvider)
    {
        this.SunsetProvider = sunsetProvider;
    }

    public DateTimeOffset Now()
    {
        return HelperTimeProvider.GetLocalNow();
    }

    public DateTimeOffset Today()
    {
        return new DateTimeOffset(
            Now().Date, Now().Offset);
    }

    public DateTimeOffset Tomorrow()
    {
        return new DateTimeOffset(
            Now().Date.AddDays(1),
            Now().Offset);
    }

    public bool IsInFuture(DateTimeOffset checkTime)
    {
        return !IsInPast(checkTime);
    }

    public bool IsInPast(DateTimeOffset checkTime)
    {
        return checkTime < Now();
    }

    public TimeSpan DurationFromNow(DateTimeOffset checkTime)
    {
        return (checkTime - Now()).Duration();
    }

    public DateTimeOffset RollForwardToNextDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Tomorrow();
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekdayDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Tomorrow();
        while (nextDay.DayOfWeek == DayOfWeek.Saturday
            || nextDay.DayOfWeek == DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }

        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekendDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Tomorrow();
        while (nextDay.DayOfWeek != DayOfWeek.Saturday
            && nextDay.DayOfWeek != DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }
}
