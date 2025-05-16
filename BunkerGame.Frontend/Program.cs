var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Подключаем поддержку статических файлов (например, index.html, css, js)
app.UseDefaultFiles(); // ищет index.html в wwwroot
app.UseStaticFiles();  // разрешает отдавать статические файлы

app.MapControllers();

// Здесь можно подключать API маршруты
// Например:
app.MapPost("/api/Game/StartGame", () => "Game started!");

// Запускаем приложение
app.Run();

app.MapPost("/api/Game/StartGame", () =>
{
    return Results.Ok(new { message = "Игра успешно запущена!" });
});

