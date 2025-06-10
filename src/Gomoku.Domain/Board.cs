namespace Gomoku.Domain;

/// <summary>
/// Represents the game board and tracks stone positions.
/// </summary>
public class Board
{
    public const int DefaultSize = 15;
    private readonly Stone?[,] _grid;

    public int Size { get; }

    public Board(int size = DefaultSize)
    {
        Size = size;
        _grid = new Stone?[size, size];
    }

    public Stone? this[int x, int y]
    {
        get => _grid[x, y];
        private set => _grid[x, y] = value;
    }

    public bool IsInside(Coordinate c) => c.X >= 0 && c.Y >= 0 && c.X < Size && c.Y < Size;

    public bool TryPlaceStone(Coordinate c, Stone stone)
    {
        if (!IsInside(c) || _grid[c.X, c.Y].HasValue)
        {
            return false;
        }
        _grid[c.X, c.Y] = stone;
        return true;
    }
}
