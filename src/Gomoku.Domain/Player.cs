namespace Gomoku.Domain;

/// <summary>
/// Value object describing a player participating in a game.
/// </summary>
public sealed record Player(Guid Id, string Name, Stone Stone);
