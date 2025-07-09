namespace PeopleViewer.Presentation.Tests;

public class PeopleViewModelTests
{
    [Fact]
    public async Task People_OnRefreshPeople_IsPopulated()
    {
        // Arrange
        var reader = new FakeReader();
        var viewModel = new PeopleViewModel(reader);

        // Act
        await viewModel.RefreshPeople();

        // Assert
        Assert.NotNull(viewModel.People);
        Assert.Equal(2, viewModel.People.Count());
    }

    [Fact]
    public async Task People_OnClearPeople_IsEmpty()
    {
        // Arrange
        var reader = new FakeReader();
        var viewModel = new PeopleViewModel(reader);
        await viewModel.RefreshPeople();
        Assert.NotEmpty(viewModel.People);

        // Act
        viewModel.ClearPeople();

        // Assert
        Assert.Empty(viewModel.People);
    }
}
