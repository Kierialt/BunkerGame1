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
                message: "Player created successfully!",
                data: player
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Type error {ex.Message}",
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
                var response = new ApiResponse<string>(true, "Story created successfully", story);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new ApiResponse<object>(false, $"Error: {ex.Message}");
                return StatusCode(500, error);
            }
        }
    }
