using Gomoku.Application;
using Gomoku.Domain;
using Orleans;

public class GameGrain : Grain, IGameGrain
{
    private GameAggregate? _game;

    public Task<GameId> StartGame(StartGameCommand command)
    {
        _game = new GameAggregate(new GameId(this.GetPrimaryKey()), command.BlackPlayer, command.WhitePlayer, command.BoardSize);
        return Task.FromResult(_game.Id);
    }

    public Task<bool> PlaceStone(PlaceStoneCommand command)
    {
        if (_game is null || _game.Id != command.GameId)
        {
            return Task.FromResult(false);
        }

        var result = _game.PlaceStone(command.Coordinate);
        return Task.FromResult(result);
    }

    public Task<GameStatusDto?> GetGameStatus()
    {
        if (_game is null)
        {
            return Task.FromResult<GameStatusDto?>(null);
        }

        var dto = new GameStatusDto(_game.Board, _game.CurrentPlayer, _game.Winner);
        return Task.FromResult<GameStatusDto?>(dto);
    }
}
