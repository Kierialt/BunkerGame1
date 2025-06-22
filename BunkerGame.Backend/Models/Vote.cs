using System.ComponentModel.DataAnnotations;

namespace BunkerGame.Backend.Models;

public class Vote
{
    [Key]
    public int Id { get; set; }
    
    public int VotingRoundId { get; set; }
    public VotingRound VotingRound { get; set; } = null!;
    
    public int VoterId { get; set; }
    public RoomPlayer Voter { get; set; } = null!;
    
    public int VotedForId { get; set; }
    public RoomPlayer VotedFor { get; set; } = null!;
    
    public DateTime VotedAt { get; set; } = DateTime.UtcNow;
} 