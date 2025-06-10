using NUnit.Framework;
using Gomoku.Domain;

namespace Gomoku.Domain.Tests;

public class GameAggregateTests
{
    [Test]
    public void BlackPlayerWins_WithVerticalFiveInARow()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var game = new GameAggregate(GameId.New(), black, white);

        game.PlaceStone(new Coordinate(0, 0)); // black
        game.PlaceStone(new Coordinate(0, 1)); // white
        game.PlaceStone(new Coordinate(1, 0)); // black
        game.PlaceStone(new Coordinate(1, 1)); // white
        game.PlaceStone(new Coordinate(2, 0)); // black
        game.PlaceStone(new Coordinate(2, 1)); // white
        game.PlaceStone(new Coordinate(3, 0)); // black
        game.PlaceStone(new Coordinate(3, 1)); // white
        game.PlaceStone(new Coordinate(4, 0)); // black wins

        Assert.IsTrue(game.IsOver);
        Assert.AreEqual(black, game.Winner);
    }

    [Test]
    public void BlackPlayerWins_WithHorizontalFiveInARow()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var game = new GameAggregate(GameId.New(), black, white);

        game.PlaceStone(new Coordinate(0, 0)); // black
        game.PlaceStone(new Coordinate(1, 0)); // white
        game.PlaceStone(new Coordinate(0, 1)); // black
        game.PlaceStone(new Coordinate(1, 1)); // white
        game.PlaceStone(new Coordinate(0, 2)); // black
        game.PlaceStone(new Coordinate(1, 2)); // white
        game.PlaceStone(new Coordinate(0, 3)); // black
        game.PlaceStone(new Coordinate(1, 3)); // white
        game.PlaceStone(new Coordinate(0, 4)); // black wins

        Assert.IsTrue(game.IsOver);
        Assert.AreEqual(black, game.Winner);
    }

    [Test]
    public void BlackPlayerWins_WithDiagonalFiveInARow()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var game = new GameAggregate(GameId.New(), black, white);

        game.PlaceStone(new Coordinate(0, 0)); // black
        game.PlaceStone(new Coordinate(0, 1)); // white
        game.PlaceStone(new Coordinate(1, 1)); // black
        game.PlaceStone(new Coordinate(0, 2)); // white
        game.PlaceStone(new Coordinate(2, 2)); // black
        game.PlaceStone(new Coordinate(0, 3)); // white
        game.PlaceStone(new Coordinate(3, 3)); // black
        game.PlaceStone(new Coordinate(0, 4)); // white
        game.PlaceStone(new Coordinate(4, 4)); // black wins

        Assert.IsTrue(game.IsOver);
        Assert.AreEqual(black, game.Winner);
    }

    [Test]
    public void NoWin_WhenSequenceBroken()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var game = new GameAggregate(GameId.New(), black, white);

        game.PlaceStone(new Coordinate(0, 0)); // black
        game.PlaceStone(new Coordinate(1, 0)); // white
        game.PlaceStone(new Coordinate(1, 1)); // black
        game.PlaceStone(new Coordinate(2, 0)); // white breaks sequence
        game.PlaceStone(new Coordinate(2, 2)); // black
        game.PlaceStone(new Coordinate(3, 0)); // white
        game.PlaceStone(new Coordinate(3, 3)); // black
        game.PlaceStone(new Coordinate(4, 0)); // white

        Assert.IsFalse(game.IsOver);
        Assert.IsNull(game.Winner);
    }
}
