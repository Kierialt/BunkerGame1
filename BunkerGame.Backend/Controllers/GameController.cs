using BunkerGame.Backend.Responses;
using Microsoft.AspNetCore.Mvc;
using BunkerGame.Backend.Services;
using BunkerGame.Backend.Models;


namespace BunkerGame.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class GameController : ControllerBase
{
    public GameController(GameService gameService, RoomService roomService)
    {
        _gameService = gameService;
        _roomService = roomService;
    }

    
   private readonly GameService _gameService;

    [HttpPost]
    public IActionResult StartGame()
    {
        try
        {

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




        private readonly RoomService _roomService;
    
        [HttpGet]
        public IActionResult RandomStory()
        {
            try
            {
                var story = _roomService.CreateRandomStory();
                var response = new ApiResponse<string>(true, "Сюжет успешно создан", story);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new ApiResponse<object>(false, $"Ошибка: {ex.Message}");
                return StatusCode(500, error);
            }
        }
    }
