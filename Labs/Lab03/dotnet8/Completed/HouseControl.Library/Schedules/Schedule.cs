﻿using HouseControl.Sunset;

namespace HouseControl.Library;

public record ScheduleFileName(string FileName) { }

public class Schedule : List<ScheduleItem>
{
    private readonly string filename;

    private IScheduleLoader? loader;
    public IScheduleLoader Loader
    {
        get => loader ??= new JsonLoader();
        set => loader = value;
    }

    private IScheduleSaver? saver;
    public IScheduleSaver Saver
    {
        get => saver ??= new JsonSaver();
        set => saver = value;
    }

    private readonly ScheduleHelper scheduleHelper;

    public Schedule(ScheduleFileName filename, ISunsetProvider sunsetProvider)
    {
        this.scheduleHelper = new ScheduleHelper(sunsetProvider);
        this.filename = filename.FileName;
        LoadSchedule();
    }

    public void LoadSchedule()
    {
        this.Clear();
        this.AddRange(Loader.LoadScheduleItems(filename));

        // update loaded schedule dates to today
        DateTimeOffset today = scheduleHelper.Today();
        foreach (var item in this)
        {
            item.Info.EventTime = today + item.Info.EventTime.TimeOfDay;
        }
        RollSchedule();
    }

    public void SaveSchedule()
    {
        Saver.SaveScheduleItems(filename, this);
    }

    public List<ScheduleItem> GetCurrentScheduleItems()
    {
        return this.Where(si => si.IsEnabled &&
            scheduleHelper.DurationFromNow(si.Info.EventTime)
            < TimeSpan.FromSeconds(30))
            .ToList();
    }

    public void RollSchedule()
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            var currentItem = this[i];
            while(scheduleHelper.IsInPast(currentItem.Info.EventTime))
            {
                if (currentItem.Info.Type == ScheduleType.Once)
                {
                    this.RemoveAt(i);
                    break;
                }

                currentItem.Info.EventTime =
                    currentItem.Info.Type switch
                    {
                        ScheduleType.Daily => scheduleHelper.RollForwardToNextDay(currentItem.Info),
                        ScheduleType.Weekday => scheduleHelper.RollForwardToNextWeekdayDay(currentItem.Info),
                        ScheduleType.Weekend => scheduleHelper.RollForwardToNextWeekendDay(currentItem.Info),
                        _ => currentItem.Info.EventTime
                    };
            }
        }
    }

}
