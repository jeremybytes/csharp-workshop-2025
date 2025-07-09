namespace HouseControl.Library.Tests;

public class ScheduleTests
{
    readonly string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\ScheduleData";

    [Fact]
    public void ScheduleItems_OnCreation_IsPopulated()
    {
        // Arrange / Act

        // Assert
        Assert.Fail("Test not implemented");
    }

    [Fact]
    public void ScheduleItems_OnCreation_AreInFuture()
    {
        // Arrange / Act

        // Assert
        Assert.Fail("Test not implemented");
    }

    [Fact]
    public void ScheduleItems_AfterRoll_AreInFuture()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("Test not implemented");
    }

    [Fact]
    public void OneTimeItemInPast_AfterRoll_IsRemoved()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("Test not implemented");
    }

    [Fact]
    public void OneTimeItemInFuture_AfterRoll_IsStillThere()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail("Test not implemented");
    }
}
