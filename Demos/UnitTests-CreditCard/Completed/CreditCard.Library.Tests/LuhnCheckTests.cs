namespace CreditCard.Library.Tests;

public class LuhnCheckTests
{
    [Theory]
    [InlineData("378282246310005")]
    [InlineData("371449635398431")]
    [InlineData("5555555555554444")]
    [InlineData("5105105105105100")]
    [InlineData("4111111111111111")]
    [InlineData("4012888888881881")]
    [InlineData("6011111111111117")]
    [InlineData("6011000990139424")]
    [InlineData("3530111333300000")]
    [InlineData("3566002020360505")]
    public void TestCardNumber_OnValidNumber_ReturnsTrue(string testNumber)
    {
        // Arrange / Act
        bool actual = LuhnCheck.TestCardNumber(testNumber);

        // Assert
        Assert.True(actual);
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("9876543210987654")]
    [InlineData("-01233454567")]
    [InlineData("7145551212")]
    [InlineData("123")]
    public void TestCardNumber_OnInvalidNumber_ReturnsFalse(string testNumber)
    {
        bool actual = LuhnCheck.TestCardNumber(testNumber);
        Assert.False(actual);
    }
}
