using NUnit.Framework;
using Gomoku.Domain;

namespace Gomoku.Domain.Tests;

public class BoardTests
{
    [Test]
    public void PlaceStone_OnEmptyCell_Succeeds()
    {
        var board = new Board();
        var coord = new Coordinate(0, 0);
        var result = board.TryPlaceStone(coord, Stone.Black);
        Assert.IsTrue(result);
        Assert.AreEqual(Stone.Black, board[0, 0]);
    }

    [Test]
    public void PlaceStone_OnOccupiedCell_Fails()
    {
        var board = new Board();
        var coord = new Coordinate(0, 0);
        board.TryPlaceStone(coord, Stone.Black);
        var result = board.TryPlaceStone(coord, Stone.White);
        Assert.IsFalse(result);
        Assert.AreEqual(Stone.Black, board[0, 0]);
    }
}
