namespace Gomoku.Domain;

/// <summary>
/// Represents the unique identifier for a game instance.
/// </summary>
public readonly record struct GameId(Guid Value)
{
    public static GameId New() => new GameId(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}
