using BunkerGame.Backend.Responses;
using Microsoft.AspNetCore.Mvc;
using BunkerGame.Backend.Services;
using BunkerGame.Backend.Models;

namespace BunkerGame.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService = new(); // в будущем можно внедрить через DI
    [HttpPost]
    public IActionResult StartGame()
    {
        try
        {
            // Здесь будет логика запуска игры
            // Пока просто успешный ответ:
            var player = _gameService.CreateRandomPlayer();

            var response = new ApiResponse<Player>(
                success: true,
                message: "Игрок успешно создан!",
                data: player
            );
           
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Если что-то пошло не так — вернем ошибку
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка типа {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }
}