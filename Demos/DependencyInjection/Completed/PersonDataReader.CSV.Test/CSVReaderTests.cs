namespace PersonDataReader.CSV.Test;

public class CSVReaderTests
{
    [Fact]
    public async Task GetPeople_WithGoodRecords_ReturnsAllRecords()
    {
        var reader = new CSVReader();
        reader.FileLoader = new FakeFileLoader("Good");

        var result = await reader.GetPeople();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetPeople_WithGoodAndBadRecords_ReturnsGoodRecords()
    {
        var reader = new CSVReader();
        reader.FileLoader = new FakeFileLoader("Mixed");

        var result = await reader.GetPeople();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetPeople_WithOnlyBadRecords_ReturnsEmptyList()
    {
        var reader = new CSVReader();
        reader.FileLoader = new FakeFileLoader("Bad");

        var result = await reader.GetPeople();

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetPeople_WithEmptyFile_ReturnsEmptyList()
    {
        var reader = new CSVReader();
        reader.FileLoader = new FakeFileLoader("Empty");

        var result = await reader.GetPeople();

        Assert.Empty(result);
    }
}
