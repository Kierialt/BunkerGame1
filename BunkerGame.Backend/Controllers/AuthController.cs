using Microsoft.AspNetCore.Mvc;
using BunkerGame.Backend.DTOs;
using BunkerGame.Backend.Services;

namespace BunkerGame.Backend.Controllers;



[ApiController]
[Route("[controller]/[action]")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        var success = await _authService.RegisterAsync(request);
        if (!success)
            return BadRequest("User already exists.");
        return Ok("Registered successfully");
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto request)
    {
        var result = await _authService.LoginAsync(request);
        if (result == null)
            return Unauthorized("Invalid Email or Password.");
        return Ok(new { Email = result });
    }
}
