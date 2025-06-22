using System.ComponentModel.DataAnnotations;

namespace BunkerGame.Backend.Models;

public class VotingRound
{
    [Key]
    public int Id { get; set; }
    
    public int GameRoomId { get; set; }
    public GameRoom GameRoom { get; set; } = null!;
    
    public int RoundNumber { get; set; }
    
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? EndedAt { get; set; }
    
    public int? EliminatedPlayerId { get; set; }
    public RoomPlayer? EliminatedPlayer { get; set; }
    
    public VotingStatus Status { get; set; } = VotingStatus.Active;
    
    // Navigation properties
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}

public enum VotingStatus
{
    Active,     // Voting active
    Finished    // Voting completed
} 