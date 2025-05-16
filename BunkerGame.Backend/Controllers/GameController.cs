using BunkerGame.Backend.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BunkerGame.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    [HttpPost]
    public IActionResult StartGame()
    {
        try
        {
            // Здесь будет логика запуска игры
            // Пока просто успешный ответ:

           var response = new ApiResponse<object>(
                success: true,
                message: "NICE",
                data: null
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