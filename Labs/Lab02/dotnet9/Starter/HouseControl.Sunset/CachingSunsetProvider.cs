﻿namespace HouseControl.Sunset;

public class CachingSunsetProvider : ISunsetProvider
{
    private readonly ISunsetProvider wrappedProvider;
    private DateTime dataDate;
    private DateTimeOffset sunrise;
    private DateTimeOffset sunset;

    public CachingSunsetProvider(ISunsetProvider sunsetProvider)
    {
        wrappedProvider = sunsetProvider;
    }

    public DateTimeOffset GetSunrise(DateTime date)
    {
        ValidateCache(date);
        return sunrise;
    }

    public DateTimeOffset GetSunset(DateTime date)
    {
        ValidateCache(date);
        return sunset;
    }

    private void ValidateCache(DateTime date)
    {
        if (dataDate != date)
        {
            sunrise = wrappedProvider.GetSunrise(date);
            sunset = wrappedProvider.GetSunset(date);
            dataDate = date;
        }
    }
}
