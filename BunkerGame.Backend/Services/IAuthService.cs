using BunkerGame.Backend.DTOs;
namespace BunkerGame.Backend.Services;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterDto request);
    Task<(string Email, string Nickname)?> LoginAsync(LoginDto request);
}