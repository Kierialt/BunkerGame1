using System.Security.Cryptography;
using System.Text;
using BunkerGame.Backend.DTOs;
using BunkerGame.Backend.Data;
using BunkerGame.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.Backend.Services;
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }
    

    public async Task<bool> RegisterAsync(RegisterDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            return false;

        using var hmac = new HMACSHA512();
        var user = new User
        {
            Nickname = request.Nickname ?? string.Empty,
            Email = request.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<(string Email, string Nickname)?> LoginAsync(LoginDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) return null;

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
        
        if (!computedHash.SequenceEqual(user.PasswordHash))
            return null;
        
        //TODO!!!!
        var nick = (Encoding.UTF8.GetBytes(request.Nickname ?? throw new InvalidOperationException()));
        if (nick == null) return null; 
        // Пока что без JWT, просто вернем Email
        return (user.Email, user.Nickname); 
        
    }
}