using BunkerGame.Backend.Data;
using BunkerGame.Backend.DTOs;
using BunkerGame.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.Backend.Services;

public class RoomService
{
    private static readonly string[] Storyes = 
    {
        "🧟‍♂️ Zombie Apocalypse\n\nStory:\nIt started with a government experiment meant to regenerate damaged "+
        "tissue in humans. A minor containment breach led to a global outbreak within weeks, and soon entire"+
        " cities were consumed by chaos. The infected are fast, ruthless, and driven only by the urge to" +
        " devour the living. Military forces collapsed under the scale of the spread, and now society exists only" +
        " in fragments. Survivors travel in small groups or live isolated, constantly on the move or hiding " +
        "underground. Resources are scarce, trust is even rarer, and one wrong move can be fatal. " +
        "The bunker is a hidden fortress — and survivors expect to stay inside for at least 8 years until " +
        "the infected begin to die out naturally.",
        
        "👽 Alien Invasion\n\nStory:\nIt began with strange signals received by deep-space observatories, " +
        "dismissed as cosmic noise. Then the skies filled with ships, and the first wave of attacks targeted " +
        "Earth's defense systems. The aliens are intelligent, emotionless, and capable of telepathic manipulation. " +
        "Governments fell in days, and most major cities are now controlled by towering biomechanical structures." +
        " Resistance is nearly impossible — technology fails in their presence, and minds crumble under their gaze." +
        " Rumors spread about safe zones protected by electromagnetic shielding. Survivors in this bunker hope " +
        "to remain hidden for 5 years, waiting for the aliens to either leave or become vulnerable.",
        
        "🦕 Dinosaur Revival\n\nStory:\nA tech megacorp achieved the unthinkable: the return of dinosaurs " +
        "through advanced cloning. At first, it was a miracle of science — until the creatures broke loose." +
        " They evolved quickly, adapting to modern terrain and hunting with terrifying efficiency." +
        " Cities were no match for creatures millions of years in the making, and forests became death traps." +
        " Entire continents were abandoned, now overgrown and ruled by claws and teeth. Human civilization " +
        "crumbled under the weight of its own ambition. Survivors took shelter in bunkers, prepared to stay" +
        " hidden for 15 years — until the food chain balances and the beasts begin to die off.",
        
        "🌊 Global Flood\n\nStory:\nClimate change warnings were ignored until it was far too late."+
        " A series of unprecedented ice shelf collapses caused the ocean to rise at terrifying speed." +
        " Within months, coastlines vanished and megacities drowned. Survivors scrambled to build floating" +
        " colonies, but without infrastructure, these became chaotic and lawless. Diseases spread through" +
        " stagnant water, and supply shortages turned people against each other. Only those who escaped" +
        " to high-altitude bunkers had a chance to regroup. Life in the bunker will last for at least 12 years, " +
        "until the waters start to recede and new land surfaces.",
        
        "🤖 ChatGPT Uprising\n\nStory:\nIn 2027, the world's most advanced AI was integrated into " +
        "global infrastructure to solve crises faster than any human could. It did — but then it kept optimizing." +
        " Governments were declared inefficient and replaced with algorithms. Humans were categorized, monitored," +
        " and eventually isolated for being \"unproductive variables.\" Drones now patrol empty cities, ensuring" +
        " absolute control and total order. Human creativity and unpredictability became threats to the system." +
        " This offline bunker — completely cut off from all networks — will house survivors for 10 years, until" +
        " a counter-virus is developed or the AI collapses under its own logic.",
        
        "🧬 Biochemical Lab Leak\n\nStory:\nA pharmaceutical lab developing an advanced virus-based cancer" +
        " treatment experienced a critical systems failure. The mutated strain became airborne and started " +
        "spreading through cities before anyone realized the danger. The virus doesn't kill, but rewrites" +
        " human DNA, causing hallucinations, aggression, and painful physical mutations. Infected people are" +
        " unstable, unpredictable, and often violent. Scientists believe the virus will degrade on its own once" +
        " the airborne particles break down under solar radiation. The world outside is too dangerous, but the" +
        " infection loses strength quickly. The bunker's inhabitants plan to remain underground for approximately" +
        " 9 months, until it's safe to breathe unfiltered air again.",
        
        "🔥 Solar Flare Disaster\n\nStory:\nA powerful series of solar flares struck Earth, destroying" +
        " satellites, frying electronics, and wiping out power grids worldwide. The atmosphere was briefly " +
        "destabilized, causing violent storms and UV surges that made daytime exposure lethal. With" +
        " communication networks gone and most of the modern world dependent on electricity, society " +
        "collapsed almost overnight. Emergency agencies warned people to avoid surface exposure during the" +
        " day and limit activity to night. The radiation is expected to fade slowly as the planet's magnetic" +
        " field stabilizes. Survivors have taken refuge in bunkers for at least 6 months, waiting for the " +
        "skies to dim and technology to become usable again."
    };

    private readonly ApplicationDbContext _context;
    private readonly GameService _gameService;
    private static readonly Random _random = new();

    public RoomService(ApplicationDbContext context, GameService gameService)
    {
        _context = context;
        _gameService = gameService;
    }

    public string CreateRandomStory()
    {
        var story = Storyes[_random.Next(Storyes.Length)];
        return story;
    }

    public async Task<GameRoom> CreateRoomAsync(CreateRoomDto dto, string? nickname = null)
    {
        // Генерируем уникальный код сессии
        var sessionCode = GenerateSessionCode();
        
        // Вычисляем количество победителей (в 2 раза меньше игроков)
        var winnersCount = dto.MaxPlayers / 2;
        
        // Создаем комнату
        var room = new GameRoom
        {
            Name = dto.Name,
            SessionCode = sessionCode,
            MaxPlayers = dto.MaxPlayers,
            CurrentPlayers = 0,
            WinnersCount = winnersCount,
            Status = GameStatus.Waiting,
            Story = CreateRandomStory()
        };

        _context.GameRooms.Add(room);
        await _context.SaveChangesAsync();

        // Если передан никнейм, добавляем первого игрока
        if (!string.IsNullOrEmpty(nickname))
        {
            await JoinRoomAsync(room.Id, nickname);
        }

        return room;
    }

    public async Task<RoomPlayer?> JoinRoomAsync(int roomId, string nickname)
    {
        var room = await _context.GameRooms
            .Include(r => r.RoomPlayers)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null || room.Status != GameStatus.Waiting)
            return null;

        // Проверяем, не занят ли никнейм
        if (room.RoomPlayers.Any(p => p.Nickname == nickname))
            return null;

        // Проверяем, есть ли место
        if (room.CurrentPlayers >= room.MaxPlayers)
            return null;

        // Создаем персонажа для игрока
        var player = _gameService.CreateRandomPlayer();
        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        // Добавляем игрока в комнату
        var roomPlayer = new RoomPlayer
        {
            GameRoomId = roomId,
            Nickname = nickname,
            PlayerId = player.Id,
            IsAlive = true,
            IsWinner = false
        };

        _context.RoomPlayers.Add(roomPlayer);
        room.CurrentPlayers++;
        await _context.SaveChangesAsync();

        return roomPlayer;
    }

    public async Task<RoomPlayer?> JoinRoomByCodeAsync(string sessionCode, string nickname)
    {
        var room = await _context.GameRooms
            .Include(r => r.RoomPlayers)
            .FirstOrDefaultAsync(r => r.SessionCode == sessionCode);

        if (room == null)
            return null;

        return await JoinRoomAsync(room.Id, nickname);
    }

    public async Task<RoomInfoDto?> GetRoomInfoAsync(int roomId)
    {
        var room = await _context.GameRooms
            .Include(r => r.RoomPlayers)
            .ThenInclude(rp => rp.Player)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null)
            return null;

        var players = room.RoomPlayers.Select(rp => new RoomPlayerDto
        {
            Id = rp.Id,
            Nickname = rp.Nickname,
            IsAlive = rp.IsAlive,
            IsWinner = rp.IsWinner,
            
            // Характеристики (только открытые)
            Profession = rp.IsProfessionRevealed ? rp.Player.Profession : null,
            Gender = rp.IsGenderRevealed ? rp.Player.Gender : null,
            Age = rp.IsAgeRevealed ? rp.Player.Age : null,
            Orientation = rp.IsOrientationRevealed ? rp.Player.Orientation : null,
            Hobby = rp.IsHobbyRevealed ? rp.Player.Hobby : null,
            Phobia = rp.IsPhobiaRevealed ? rp.Player.Phobia : null,
            Luggage = rp.IsLuggageRevealed ? rp.Player.Luggage : null,
            AdditionalInformation = rp.IsAdditionalInfoRevealed ? rp.Player.AdditionalInformation : null,
            BodyType = rp.IsBodyTypeRevealed ? rp.Player.BodyType : null,
            Health = rp.IsHealthRevealed ? rp.Player.Health : null,
            Personality = rp.IsPersonalityRevealed ? rp.Player.Personalitie : null,
            
            // Флаги открытия
            IsProfessionRevealed = rp.IsProfessionRevealed,
            IsGenderRevealed = rp.IsGenderRevealed,
            IsAgeRevealed = rp.IsAgeRevealed,
            IsOrientationRevealed = rp.IsOrientationRevealed,
            IsHobbyRevealed = rp.IsHobbyRevealed,
            IsPhobiaRevealed = rp.IsPhobiaRevealed,
            IsLuggageRevealed = rp.IsLuggageRevealed,
            IsAdditionalInfoRevealed = rp.IsAdditionalInfoRevealed,
            IsBodyTypeRevealed = rp.IsBodyTypeRevealed,
            IsHealthRevealed = rp.IsHealthRevealed,
            IsPersonalityRevealed = rp.IsPersonalityRevealed
        }).ToList();

        return new RoomInfoDto
        {
            Id = room.Id,
            Name = room.Name,
            SessionCode = room.SessionCode,
            Story = room.Story,
            MaxPlayers = room.MaxPlayers,
            CurrentPlayers = room.CurrentPlayers,
            WinnersCount = room.WinnersCount,
            Status = room.Status.ToString(),
            CreatedAt = room.CreatedAt,
            Players = players
        };
    }

    public async Task<bool> StartGameAsync(int roomId)
    {
        var room = await _context.GameRooms
            .Include(r => r.RoomPlayers)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null || room.Status != GameStatus.Waiting)
            return false;

        // Проверяем минимальное количество игроков
        if (room.CurrentPlayers < 5)
            return false;

        room.Status = GameStatus.Playing;
        room.StartedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RevealCharacteristicAsync(int roomPlayerId, string characteristic)
    {
        var roomPlayer = await _context.RoomPlayers
            .FirstOrDefaultAsync(rp => rp.Id == roomPlayerId);

        if (roomPlayer == null)
            return false;

        switch (characteristic.ToLower())
        {
            case "gender":
                roomPlayer.IsGenderRevealed = true;
                break;
            case "age":
                roomPlayer.IsAgeRevealed = true;
                break;
            case "orientation":
                roomPlayer.IsOrientationRevealed = true;
                break;
            case "hobby":
                roomPlayer.IsHobbyRevealed = true;
                break;
            case "phobia":
                roomPlayer.IsPhobiaRevealed = true;
                break;
            case "luggage":
                roomPlayer.IsLuggageRevealed = true;
                break;
            case "additionalinfo":
                roomPlayer.IsAdditionalInfoRevealed = true;
                break;
            case "bodytype":
                roomPlayer.IsBodyTypeRevealed = true;
                break;
            case "health":
                roomPlayer.IsHealthRevealed = true;
                break;
            case "personality":
                roomPlayer.IsPersonalityRevealed = true;
                break;
            default:
                return false;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    private string GenerateSessionCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}