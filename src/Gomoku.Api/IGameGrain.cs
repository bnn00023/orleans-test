using Gomoku.Application;
using Gomoku.Domain;
using Orleans;

public interface IGameGrain : IGrainWithGuidKey
{
    Task<GameId> StartGame(StartGameCommand command);
    Task<bool> PlaceStone(PlaceStoneCommand command);
    Task<GameStatusDto?> GetGameStatus();
}
