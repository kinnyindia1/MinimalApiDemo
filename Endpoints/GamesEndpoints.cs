using Microsoft.EntityFrameworkCore;
using MinimalApiDemo.Data;
using MinimalApiDemo.Models;

namespace MinimalApiDemo.Endpoints
{
    public static class GamesEndpoints
    {
        public static RouteGroupBuilder MapGamesEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (GameStoreContext db) =>
                await db.Games.ToListAsync());

            group.MapGet("/{id:int}", async (int id, GameStoreContext db) =>
                await db.Games.FindAsync(id)
                    is Game game
                        ? Results.Ok(game)
                        : Results.NotFound());

            group.MapPost("/", async (Game game, GameStoreContext db) =>
            {
                db.Games.Add(game);
                await db.SaveChangesAsync();
                return Results.Created($"/games/{game.Id}", game);
            });

            group.MapPut("/{id:int}", async (int id, Game inputGame, GameStoreContext db) =>
            {
                var game = await db.Games.FindAsync(id);
                if (game is null) return Results.NotFound();
                game.Name = inputGame.Name;
                game.Genre = inputGame.Genre;
                game.Price = inputGame.Price;
                game.ReleaseDate = inputGame.ReleaseDate;
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, GameStoreContext db) =>
            {
                if (await db.Games.FindAsync(id) is Game game)
                {
                    db.Games.Remove(game);
                    await db.SaveChangesAsync();
                    return Results.Ok(game);
                }
                return Results.NotFound();
            });
            return group;
        }
    }
}
