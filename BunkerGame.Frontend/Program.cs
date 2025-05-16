var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();

// app.MapPost("/api/game/start", () => Results.Ok("Игра началась!"));
