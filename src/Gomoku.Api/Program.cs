using Gomoku.Application;
using Gomoku.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseOrleans(silo =>
{
    silo.UseLocalhostClustering();
});

var app = builder.Build();

app.MapPost("/games", async (IGrainFactory grains, StartGameCommand command) =>
{
    var id = Guid.NewGuid();
    var grain = grains.GetGrain<IGameGrain>(id);
    var gameId = await grain.StartGame(command);
    return Results.Ok(gameId);
});

app.MapPost("/games/{id:guid}/stones", async (IGrainFactory grains, Guid id, Coordinate coordinate) =>
{
    var grain = grains.GetGrain<IGameGrain>(id);
    var placed = await grain.PlaceStone(new PlaceStoneCommand(new GameId(id), coordinate));
    return Results.Ok(placed);
});

app.MapGet("/games/{id:guid}", async (IGrainFactory grains, Guid id) =>
{
    var grain = grains.GetGrain<IGameGrain>(id);
    var status = await grain.GetGameStatus();
    return status is not null ? Results.Ok(status) : Results.NotFound();
});

app.Run();

