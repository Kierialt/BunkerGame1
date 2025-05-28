using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.Backend.Models;

public class User
{
    
    public int Id { get; set; }
    [Required]
    [MaxLength(255)] // Ограничение длины строки
    [EmailAddress]   // Проверка на валидный e-mail (необязательно, но полезно)
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    
}

