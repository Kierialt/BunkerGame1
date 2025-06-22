namespace BunkerGame.Backend.DTOs;

public class CreateRoomDto
{
    public string Name { get; set; } = default!;
    public int MaxPlayers { get; set; }
    public string? Nickname { get; set; }
}

public class JoinRoomDto
{
    public string SessionCode { get; set; } = default!;
    public string Nickname { get; set; } = default!;
}

public class RoomInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string SessionCode { get; set; } = default!;
    public string? Story { get; set; }
    public int MaxPlayers { get; set; }
    public int CurrentPlayers { get; set; }
    public int WinnersCount { get; set; }
    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public List<RoomPlayerDto> Players { get; set; } = new();
}

public class RoomPlayerDto
{
    public int Id { get; set; }
    public string Nickname { get; set; } = default!;
    public bool IsAlive { get; set; }
    public bool IsWinner { get; set; }
    
    // Характеристики игрока (только те, что открыты)
    public string? Profession { get; set; }
    public string? Gender { get; set; }
    public int? Age { get; set; }
    public string? Orientation { get; set; }
    public string? Hobby { get; set; }
    public string? Phobia { get; set; }
    public string? Luggage { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? BodyType { get; set; }
    public string? Health { get; set; }
    public string? Personality { get; set; }
    
    // Флаги открытия характеристик
    public bool IsProfessionRevealed { get; set; }
    public bool IsGenderRevealed { get; set; }
    public bool IsAgeRevealed { get; set; }
    public bool IsOrientationRevealed { get; set; }
    public bool IsHobbyRevealed { get; set; }
    public bool IsPhobiaRevealed { get; set; }
    public bool IsLuggageRevealed { get; set; }
    public bool IsAdditionalInfoRevealed { get; set; }
    public bool IsBodyTypeRevealed { get; set; }
    public bool IsHealthRevealed { get; set; }
    public bool IsPersonalityRevealed { get; set; }
}

public class VoteDto
{
    public int VotedForId { get; set; }
}

public class VotingRoundDto
{
    public int Id { get; set; }
    public int RoundNumber { get; set; }
    public string Status { get; set; } = default!;
    public DateTime StartedAt { get; set; }
    public int? EliminatedPlayerId { get; set; }
    public string? EliminatedPlayerNickname { get; set; }
    public List<VoteInfoDto> Votes { get; set; } = new();
}

public class VoteInfoDto
{
    public string VoterNickname { get; set; } = default!;
    public string VotedForNickname { get; set; } = default!;
    public DateTime VotedAt { get; set; }
} 