namespace Gomoku.Application;

using Gomoku.Domain;

/// <summary>
/// Query to retrieve the current board state and game result.
/// </summary>
public sealed record GetGameStatusQuery(GameId GameId);
