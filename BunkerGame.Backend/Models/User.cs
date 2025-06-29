using System.ComponentModel.DataAnnotations;


namespace BunkerGame.Backend.Models;

public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(255)] 
    [EmailAddress]   
    public string Email { get; set; } = default!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    [Required]
    [MaxLength(255)] 
    
    public string Nickname { get; set; } = default!;
}

