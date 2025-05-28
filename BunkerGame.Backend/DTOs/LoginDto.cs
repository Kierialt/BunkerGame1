namespace BunkerGame.Backend.DTOs;

public class LoginDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string? Nickname { get; set; } = default!;
}