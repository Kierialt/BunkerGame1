using System.ComponentModel.DataAnnotations;

namespace BunkerGame.Backend.Models;

public class RoomPlayer
{
    [Key]
    public int Id { get; set; }
    
    public int GameRoomId { get; set; }
    public GameRoom GameRoom { get; set; } = null!;
    
    public int? UserId { get; set; }
    public User? User { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Nickname { get; set; } = default!;
    
    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    
    public bool IsAlive { get; set; } = true;
    
    public bool IsWinner { get; set; } = false;
    
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? LeftAt { get; set; }
    
    // Скрытые характеристики (открываются постепенно)
    public bool IsProfessionRevealed { get; set; } = true; // Профессия видна сразу
    public bool IsGenderRevealed { get; set; } = false;
    public bool IsAgeRevealed { get; set; } = false;
    public bool IsOrientationRevealed { get; set; } = false;
    public bool IsHobbyRevealed { get; set; } = false;
    public bool IsPhobiaRevealed { get; set; } = false;
    public bool IsLuggageRevealed { get; set; } = false;
    public bool IsAdditionalInfoRevealed { get; set; } = false;
    public bool IsBodyTypeRevealed { get; set; } = false;
    public bool IsHealthRevealed { get; set; } = false;
    public bool IsPersonalityRevealed { get; set; } = false;
} 