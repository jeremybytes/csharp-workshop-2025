namespace HouseControl.Library.Tests;

public class ScheduleTests
{
    readonly ScheduleFileName fileName = new(AppDomain.CurrentDomain.BaseDirectory + "\\ScheduleData");

    [Fact]
    public void ScheduleItems_OnCreation_IsPopulated()
    {
        // Arrange / Act
        var schedule = new Schedule(fileName, new FakeSunsetProvider());

        // Assert
        Assert.NotEmpty(schedule);
    }

    [Fact]
    public void ScheduleItems_OnCreation_AreInFuture()
    {
        // Arrange / Act
        var schedule = new Schedule(fileName, new FakeSunsetProvider());
        var currentTime = DateTimeOffset.Now;

        // Assert
        foreach (var item in schedule)
        {
            Assert.True(item.Info.EventTime > currentTime);
        }
    }

    [Fact]
    public void ScheduleItems_AfterRoll_AreInFuture()
    {
        // Arrange
        var schedule = new Schedule(fileName, new FakeSunsetProvider());
        var currentTime = DateTimeOffset.Now;
        foreach (var item in schedule)
        {
            item.Info.EventTime = item.Info.EventTime.AddDays(-30);
            Assert.True(item.Info.EventTime < currentTime,
                "Invalid Arrangement");
        }

        // Act
        schedule.RollSchedule();

        // Assert
        foreach (var item in schedule)
        {
            Assert.True(item.Info.EventTime > currentTime);
        }
    }

    [Fact]
    public void OneTimeItemInPast_AfterRoll_IsRemoved()
    {
        // Arrange
        var schedule = new Schedule(fileName, new FakeSunsetProvider());

        var originalCount = schedule.Count;

        var newItem = new ScheduleItem(
            1,
            DeviceCommand.On,
            new ScheduleInfo()
            {
                EventTime = DateTimeOffset.Now.AddMinutes(-2),
                Type = ScheduleType.Once,
            },
            true,
            "");
        schedule.Add(newItem);

        var newCount = schedule.Count;
        Assert.True(newCount == (originalCount + 1),
            "Invalid Arrangement");

        // Act
        schedule.RollSchedule();

        // Assert
        Assert.Equal(originalCount, schedule.Count);
    }

    [Fact]
    public void OneTimeItemInFuture_AfterRoll_IsStillThere()
    {
        // Arrange
        var schedule = new Schedule(fileName, new FakeSunsetProvider());

        var originalCount = schedule.Count;

        var newItem = new ScheduleItem(
            1,
            DeviceCommand.On,
            new ScheduleInfo()
            {
                EventTime = DateTimeOffset.Now.AddMinutes(+2),
                Type = ScheduleType.Once,
            },
            true,
            "");
        schedule.Add(newItem);

        var newCount = schedule.Count;
        Assert.True(newCount == (originalCount + 1),
            "Invalid Arrangement");

        // Act
        schedule.RollSchedule();

        // Assert
        Assert.Equal(newCount, schedule.Count);
    }
}
