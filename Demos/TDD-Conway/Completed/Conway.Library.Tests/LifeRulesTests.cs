namespace Conway.Library.Tests;

public class LifeRulesTests
{
    /*
     Conway's Game of Life
    -------------------------
    Any live cell with fewer than two live neighbours dies.
    Any live cell with two or three live neighbours lives.
    Any live cell with more than three live neighbours dies.
    Any dead cell with exactly three live neighbours becomes a live cell.
    */

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void LiveCell_FewerThan2LiveNeighbors_Dies(int neighbors)
    {
        // Arrange
        CellState currentState = CellState.Alive;

        // Act
        var newState = LifeRules.GetNewState(currentState, neighbors);

        // Assert
        Assert.Equal(CellState.Dead, newState);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    public void LiveCell_2or3LiveNeighbors_Lives(int neighbors)
    {
        CellState currentState = CellState.Alive;
        var newState = LifeRules.GetNewState(currentState, neighbors);
        Assert.Equal(CellState.Alive, newState);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void LiveCell_MoreThan3LiveNeighbors_Dies(int neighbors)
    {
        CellState currentState = CellState.Alive;
        var newState = LifeRules.GetNewState(currentState, neighbors);
        Assert.Equal(CellState.Dead, newState);
    }

    [Fact]
    public void DeadCell_Exactly3LiveNeighbors_Lives()
    {
        CellState currentState = CellState.Dead;
        int neighbors = 3;
        var newState = LifeRules.GetNewState(currentState, neighbors);
        Assert.Equal(CellState.Alive, newState);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void DeadCell_LessThan3LiveNeighbors_StaysDead(int neighbors)
    {
        CellState currentState = CellState.Dead;
        var newState = LifeRules.GetNewState(currentState, neighbors);
        Assert.Equal(CellState.Dead, newState);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void DeadCell_MoreThan3LiveNeighbors_StaysDead(int neighbors)
    {
        CellState currentState = CellState.Dead;
        var newState = LifeRules.GetNewState(currentState, neighbors);
        Assert.Equal(CellState.Dead, newState);
    }

    [Fact]
    public void AnyCell_LessThan0LiveNeighbors_ThrowsException()
    {
        int neighbors = -1;

        Assert.Throws<ArgumentOutOfRangeException>("liveNeighbors",
            () => LifeRules.GetNewState(CellState.Alive, neighbors));
        Assert.Throws<ArgumentOutOfRangeException>("liveNeighbors",
            () => LifeRules.GetNewState(CellState.Dead, neighbors));
    }

    [Fact]
    public void AnyCell_MoreThan8LiveNeighbors_ThrowsException()
    {
        int neighbors = 9;

        Assert.Throws<ArgumentOutOfRangeException>("liveNeighbors",
            () => LifeRules.GetNewState(CellState.Alive, neighbors));
        Assert.Throws<ArgumentOutOfRangeException>("liveNeighbors",
            () => LifeRules.GetNewState(CellState.Dead, neighbors));
    }

    [Fact]
    public void InvalidCellState_ThrowsException()
    {
        CellState currentState = (CellState)2;
        int neighbors = 3;

        Assert.Throws<ArgumentOutOfRangeException>("currentState",
            () => LifeRules.GetNewState(currentState, neighbors));
    }

}
