using BunkerGame.Backend.Services;
using BunkerGame.Backend.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins( "https://localhost:7258","http://localhost:5198")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<VotingService>();


// Get the connection string from environment variable or from appsettings.Production/Development.json
string dbPath = Path.Combine(AppContext.BaseDirectory, builder.Configuration.GetValue<string>("DbPath") ?? throw new InvalidOperationException());
string connectionString = $"Data Source={dbPath}";

// Configure context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));


// Register authorization service
builder.Services.AddScoped<IAuthService, AuthService>();

// Register backup service
builder.Services.AddHostedService<BackupService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
//     
// .WithName("GetWeatherForecast")
// .WithOpenApi();


app.UseCors("AllowAll");


app.MapControllers();
app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
