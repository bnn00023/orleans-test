namespace Gomoku.Application;

using Gomoku.Domain;
using System.Collections.Generic;

/// <summary>
/// Application service coordinating commands and queries.
/// </summary>
public class GameService
{
    private readonly Dictionary<GameId, GameAggregate> _games = new();

    /// <summary>
    /// Starts a new game and returns its identifier.
    /// </summary>
    public GameId StartGame(StartGameCommand command)
    {
        var id = GameId.New();
        var game = new GameAggregate(id, command.BlackPlayer, command.WhitePlayer, command.BoardSize);
        _games[id] = game;
        return id;
    }

    /// <summary>
    /// Places a stone for the current player.
    /// </summary>
    public bool PlaceStone(PlaceStoneCommand command)
    {
        if (!_games.TryGetValue(command.GameId, out var game))
        {
            return false;
        }
        return game.PlaceStone(command.Coordinate);
    }

    /// <summary>
    /// Retrieves the current game status.
    /// </summary>
    public GameStatusDto? GetStatus(GetGameStatusQuery query)
    {
        if (!_games.TryGetValue(query.GameId, out var game))
        {
            return null;
        }
        return new GameStatusDto(game.Board, game.CurrentPlayer, game.Winner);
    }
}
