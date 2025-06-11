namespace Gomoku.Application;

using Gomoku.Domain;

/// <summary>
/// Command to place a stone for the active player.
/// </summary>
public sealed record PlaceStoneCommand(GameId GameId, Coordinate Coordinate);
