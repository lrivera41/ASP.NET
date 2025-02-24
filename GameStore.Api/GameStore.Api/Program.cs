using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string GateGameEndpointName = "GetGame";
app.UseHttpsRedirection();

List<GameDto> games = [
    new (
        1,
        "The Last of Us Part II",
        "Action",
        59.99m,
        new DateOnly(2020, 6, 19)),
    new (
        2,
        "Ghost of Tsushima",
        "Action",
        59.99m,
        new DateOnly(2020, 7, 17)),
    new (
        3,
        "Cyberpunk 2077",
        "RPG",
        59.99m, 
        new DateOnly(2020, 12, 10))
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/1
app.MapGet("/games/{id}", (int id) => 
{
    GameDto? game = games.Find(g => g.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GateGameEndpointName);

// POST /games
app.MapPost("/games", (CreateGameDto newGame) => 
{
    var game = new GameDto(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

    games.Add(game);

    return Results.CreatedAtRoute(GateGameEndpointName, new { id = game.Id }, game);
});

// PUT /games/1
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) => 
{
    var index = games.FindIndex(g => g.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate);

    return Results.NoContent();
});

// DELETE /games/1
app.MapDelete("/games/{id}", (int id) => 
{
    games.RemoveAll(games => games.Id == id);

    return Results.NoContent();
});

app.Run();

