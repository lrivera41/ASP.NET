using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
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
app.MapGet("/games/{id}", (int id) => games.FirstOrDefault(g => g.Id == id));

app.Run();

