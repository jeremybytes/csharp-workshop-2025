namespace Conway.Library;

public enum CellState
{
    Alive,
    Dead,
}

public class LifeRules
{
    public static CellState GetNewState(
        CellState currentState, int liveNeighbors)
    {
        if (liveNeighbors < 0 || liveNeighbors > 8)
        {
            throw new ArgumentOutOfRangeException(nameof(liveNeighbors));
        }

        return (currentState, liveNeighbors) switch
        {
            (CellState.Alive, 2) => CellState.Alive,
            (CellState.Alive, 3) => CellState.Alive,
            (CellState.Alive, _) => CellState.Dead,
            (CellState.Dead, 3) => CellState.Alive,
            (CellState.Dead, _) => CellState.Dead,
            (_, _) => throw new ArgumentOutOfRangeException(nameof(currentState)),
        };
    }
}
