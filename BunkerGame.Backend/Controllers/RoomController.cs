using Microsoft.AspNetCore.Mvc;
using BunkerGame.Backend.Services;

namespace BunkerGame.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult RandomStory()
    {
        var story = _roomService.CreateRandomStory();
        return Ok(new { data = story });
    }
}