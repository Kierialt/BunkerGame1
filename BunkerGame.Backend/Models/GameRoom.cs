using System.ComponentModel.DataAnnotations;

namespace BunkerGame.Backend.Models;

public class GameRoom
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = default!;
    
    [Required]
    public string SessionCode { get; set; } = default!;
    
    public string? Story { get; set; }
    
    public int MaxPlayers { get; set; }
    
    public int CurrentPlayers { get; set; }
    
    public int WinnersCount { get; set; }
    
    public GameStatus Status { get; set; } = GameStatus.Waiting;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? StartedAt { get; set; }
    
    public DateTime? EndedAt { get; set; }
    
    // Navigation properties
    public ICollection<RoomPlayer> RoomPlayers { get; set; } = new List<RoomPlayer>();
    public ICollection<VotingRound> VotingRounds { get; set; } = new List<VotingRound>();
}

public enum GameStatus
{
    Waiting,    // Ожидание игроков
    Playing,    // Игра идет
    Finished    // Игра завершена
} 