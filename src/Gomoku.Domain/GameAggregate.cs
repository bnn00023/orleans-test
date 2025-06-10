namespace Gomoku.Domain;

/// <summary>
/// Aggregate root managing board state, players and game flow.
/// </summary>
public class GameAggregate
{
    private readonly Player[] _players;
    private readonly WinRule _winRule;

    public GameId Id { get; }
    public Board Board { get; }
    public int TurnIndex { get; private set; }
    public Player CurrentPlayer => _players[TurnIndex];
    public Player? Winner { get; private set; }
    public bool IsOver => Winner is not null;

    public GameAggregate(GameId id, Player black, Player white, int boardSize = Board.DefaultSize)
    {
        Id = id;
        _players = new[] { black, white };
        Board = new Board(boardSize);
        _winRule = new WinRule();
        TurnIndex = 0;
    }

    public bool PlaceStone(Coordinate coordinate)
    {
        if (IsOver || !Board.TryPlaceStone(coordinate, CurrentPlayer.Stone))
        {
            return false;
        }

        if (_winRule.IsWinningMove(Board, coordinate, CurrentPlayer.Stone))
        {
            Winner = CurrentPlayer;
        }
        else
        {
            TurnIndex = 1 - TurnIndex;
        }

        return true;
    }
}
