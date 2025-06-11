namespace Gomoku.Application;

using Gomoku.Domain;

/// <summary>
/// DTO representing the status of a game.
/// </summary>
public sealed record GameStatusDto(Board Board, Player CurrentPlayer, Player? Winner);
