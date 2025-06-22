using BunkerGame.Backend.Data;
using BunkerGame.Backend.DTOs;
using BunkerGame.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace BunkerGame.Backend.Services;

public class VotingService
{
    private readonly ApplicationDbContext _context;

    public VotingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<VotingRound?> StartVotingRoundAsync(int roomId)
    {
        var room = await _context.GameRooms
            .Include(r => r.RoomPlayers.Where(p => p.IsAlive))
            .Include(r => r.VotingRounds)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null || room.Status != GameStatus.Playing)
            return null;

        // Проверяем, есть ли активное голосование
        var activeVoting = room.VotingRounds.FirstOrDefault(v => v.Status == VotingStatus.Active);
        if (activeVoting != null)
            return activeVoting;

        // Проверяем, сколько игроков осталось
        var alivePlayers = room.RoomPlayers.Where(p => p.IsAlive).ToList();
        if (alivePlayers.Count <= room.WinnersCount)
        {
            // Игра окончена, определяем победителей
            await EndGameAsync(roomId);
            return null;
        }

        // Создаем новый раунд голосования
        var roundNumber = room.VotingRounds.Count + 1;
        var votingRound = new VotingRound
        {
            GameRoomId = roomId,
            RoundNumber = roundNumber,
            Status = VotingStatus.Active,
            StartedAt = DateTime.UtcNow
        };

        _context.VotingRounds.Add(votingRound);
        await _context.SaveChangesAsync();

        return votingRound;
    }

    public async Task<bool> VoteAsync(int votingRoundId, int voterId, int votedForId)
    {
        var votingRound = await _context.VotingRounds
            .Include(v => v.GameRoom)
            .ThenInclude(r => r.RoomPlayers.Where(p => p.IsAlive))
            .FirstOrDefaultAsync(v => v.Id == votingRoundId);

        if (votingRound == null || votingRound.Status != VotingStatus.Active)
            return false;

        // Проверяем, что голосующий жив
        var voter = votingRound.GameRoom.RoomPlayers.FirstOrDefault(p => p.Id == voterId && p.IsAlive);
        if (voter == null)
            return false;

        // Проверяем, что за кого голосуют - жив
        var votedFor = votingRound.GameRoom.RoomPlayers.FirstOrDefault(p => p.Id == votedForId && p.IsAlive);
        if (votedFor == null)
            return false;

        // Проверяем, не голосовал ли уже этот игрок
        var existingVote = await _context.Votes
            .FirstOrDefaultAsync(v => v.VotingRoundId == votingRoundId && v.VoterId == voterId);

        if (existingVote != null)
        {
            // Обновляем существующий голос
            existingVote.VotedForId = votedForId;
            existingVote.VotedAt = DateTime.UtcNow;
        }
        else
        {
            // Создаем новый голос
            var vote = new Vote
            {
                VotingRoundId = votingRoundId,
                VoterId = voterId,
                VotedForId = votedForId,
                VotedAt = DateTime.UtcNow
            };
            _context.Votes.Add(vote);
        }

        await _context.SaveChangesAsync();

        // Проверяем, все ли проголосовали
        await CheckVotingCompletionAsync(votingRoundId);

        return true;
    }

    public async Task<VotingRoundDto?> GetVotingRoundAsync(int votingRoundId)
    {
        var votingRound = await _context.VotingRounds
            .Include(v => v.Votes)
            .ThenInclude(v => v.Voter)
            .Include(v => v.Votes)
            .ThenInclude(v => v.VotedFor)
            .Include(v => v.EliminatedPlayer)
            .FirstOrDefaultAsync(v => v.Id == votingRoundId);

        if (votingRound == null)
            return null;

        var votes = votingRound.Votes.Select(v => new VoteInfoDto
        {
            VoterNickname = v.Voter.Nickname,
            VotedForNickname = v.VotedFor.Nickname,
            VotedAt = v.VotedAt
        }).ToList();

        return new VotingRoundDto
        {
            Id = votingRound.Id,
            RoundNumber = votingRound.RoundNumber,
            Status = votingRound.Status.ToString(),
            StartedAt = votingRound.StartedAt,
            EliminatedPlayerId = votingRound.EliminatedPlayerId,
            EliminatedPlayerNickname = votingRound.EliminatedPlayer?.Nickname,
            Votes = votes
        };
    }

    public async Task<VotingRoundDto?> GetCurrentVotingRoundAsync(int roomId)
    {
        var votingRound = await _context.VotingRounds
            .Include(v => v.Votes)
            .ThenInclude(v => v.Voter)
            .Include(v => v.Votes)
            .ThenInclude(v => v.VotedFor)
            .Include(v => v.EliminatedPlayer)
            .Where(v => v.GameRoomId == roomId)
            .OrderByDescending(v => v.RoundNumber)
            .FirstOrDefaultAsync();

        if (votingRound == null)
            return null;

        var votes = votingRound.Votes.Select(v => new VoteInfoDto
        {
            VoterNickname = v.Voter.Nickname,
            VotedForNickname = v.VotedFor.Nickname,
            VotedAt = v.VotedAt
        }).ToList();

        return new VotingRoundDto
        {
            Id = votingRound.Id,
            RoundNumber = votingRound.RoundNumber,
            Status = votingRound.Status.ToString(),
            StartedAt = votingRound.StartedAt,
            EliminatedPlayerId = votingRound.EliminatedPlayerId,
            EliminatedPlayerNickname = votingRound.EliminatedPlayer?.Nickname,
            Votes = votes
        };
    }

    private async Task CheckVotingCompletionAsync(int votingRoundId)
    {
        var votingRound = await _context.VotingRounds
            .Include(v => v.GameRoom)
            .ThenInclude(r => r.RoomPlayers.Where(p => p.IsAlive))
            .Include(v => v.Votes)
            .FirstOrDefaultAsync(v => v.Id == votingRoundId);

        if (votingRound == null)
            return;

        var alivePlayers = votingRound.GameRoom.RoomPlayers.Where(p => p.IsAlive).ToList();
        var votes = votingRound.Votes.ToList();

        // Если все живые игроки проголосовали
        if (votes.Count >= alivePlayers.Count)
        {
            await CompleteVotingRoundAsync(votingRoundId);
        }
    }

    private async Task CompleteVotingRoundAsync(int votingRoundId)
    {
        var votingRound = await _context.VotingRounds
            .Include(v => v.Votes)
            .ThenInclude(v => v.VotedFor)
            .FirstOrDefaultAsync(v => v.Id == votingRoundId);

        if (votingRound == null)
            return;

        // Подсчитываем голоса
        var voteCounts = votingRound.Votes
            .GroupBy(v => v.VotedForId)
            .Select(g => new { PlayerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        if (voteCounts.Any())
        {
            // Исключаем игрока с наибольшим количеством голосов
            var eliminatedPlayerId = voteCounts.First().PlayerId;
            var eliminatedPlayer = await _context.RoomPlayers
                .FirstOrDefaultAsync(p => p.Id == eliminatedPlayerId);

            if (eliminatedPlayer != null)
            {
                eliminatedPlayer.IsAlive = false;
                votingRound.EliminatedPlayerId = eliminatedPlayerId;
            }
        }

        votingRound.Status = VotingStatus.Finished;
        votingRound.EndedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    private async Task EndGameAsync(int roomId)
    {
        var room = await _context.GameRooms
            .Include(r => r.RoomPlayers.Where(p => p.IsAlive))
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room == null)
            return;

        // Определяем победителей (оставшихся живых игроков)
        var winners = room.RoomPlayers.Where(p => p.IsAlive).Take(room.WinnersCount).ToList();
        foreach (var winner in winners)
        {
            winner.IsWinner = true;
        }

        room.Status = GameStatus.Finished;
        room.EndedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }
} 