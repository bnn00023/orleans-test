using NUnit.Framework;
using Gomoku.Domain;
using Gomoku.Application;

namespace Gomoku.Application.Tests;

public class GameServiceTests
{
    [Test]
    public void StartGame_Returns_GameId_AndInitialStatus()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var service = new GameService();

        var id = service.StartGame(new StartGameCommand(black, white));
        var status = service.GetStatus(new GetGameStatusQuery(id));

        Assert.IsNotNull(status);
        Assert.AreEqual(Stone.Black, status!.CurrentPlayer.Stone);
        Assert.IsNull(status.Winner);
        Assert.IsNull(status.Board[0,0]);
    }

    [Test]
    public void PlaceStone_UpdatesBoard_AndSwitchesTurn()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var service = new GameService();
        var id = service.StartGame(new StartGameCommand(black, white));

        var placed = service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(0,0)));
        var status = service.GetStatus(new GetGameStatusQuery(id));

        Assert.IsTrue(placed);
        Assert.AreEqual(Stone.Black, status!.Board[0,0]);
        Assert.AreEqual(Stone.White, status.CurrentPlayer.Stone);
    }

    [Test]
    public void PlaceStone_WinningMove_SetsWinner()
    {
        var black = new Player(Guid.NewGuid(), "B", Stone.Black);
        var white = new Player(Guid.NewGuid(), "W", Stone.White);
        var service = new GameService();
        var id = service.StartGame(new StartGameCommand(black, white));

        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(0,0))); // black
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(1,0))); // white
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(0,1))); // black
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(1,1))); // white
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(0,2))); // black
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(1,2))); // white
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(0,3))); // black
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(1,3))); // white
        service.PlaceStone(new PlaceStoneCommand(id, new Coordinate(0,4))); // black wins

        var status = service.GetStatus(new GetGameStatusQuery(id));

        Assert.IsNotNull(status!.Winner);
        Assert.AreEqual(black, status.Winner);
    }
}
