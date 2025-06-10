namespace Gomoku.Domain;

/// <summary>
/// Encapsulates the logic for detecting a winning sequence.
/// </summary>
public class WinRule
{
    private readonly int _winLength;

    public WinRule(int winLength = 5)
    {
        _winLength = winLength;
    }

    public bool IsWinningMove(Board board, Coordinate lastMove, Stone stone)
    {
        int[][] directions = new int[][]
        {
            new[] {1, 0},
            new[] {0, 1},
            new[] {1, 1},
            new[] {1, -1}
        };

        foreach (var dir in directions)
        {
            int count = 1;
            count += Count(board, lastMove, stone, dir[0], dir[1]);
            count += Count(board, lastMove, stone, -dir[0], -dir[1]);
            if (count >= _winLength)
            {
                return true;
            }
        }

        return false;
    }

    private int Count(Board board, Coordinate from, Stone stone, int dx, int dy)
    {
        int x = from.X + dx;
        int y = from.Y + dy;
        int count = 0;
        while (board.IsInside(new Coordinate(x, y)) && board[x, y] == stone)
        {
            count++;
            x += dx;
            y += dy;
        }
        return count;
    }
}
