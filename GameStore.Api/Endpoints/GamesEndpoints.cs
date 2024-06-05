using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GamesDto> games = [
                new(
            1,
            "Mario",
            "Running",
            19.99M,
            new DateOnly(1992,7,15)
            ),
            new(
            2,
            "Street Fighter II",
            "Fighting",
            59.99M,
            new DateOnly(1990,3,10)
            ),
            new(
            3,
            "Fifa",
            "Sports",
            69.99M,
            new DateOnly(2022,9,27)
            ),
        ];
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();
        //GET /games
        group.MapGet("/", () => games);

        //GET /games/1
        group.MapGet("/{id}", (int id) =>
        {
            GamesDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGameEndpointName);

        //POST /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GamesDto game = new(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        //PUT /games/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            int index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GamesDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        //DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
        return group;
    }
}
