namespace Gomoku.Application;

using Gomoku.Domain;

/// <summary>
/// Command to create a new game and initialize the board.
/// </summary>
public sealed record StartGameCommand(Player BlackPlayer, Player WhitePlayer, int BoardSize = Board.DefaultSize);
