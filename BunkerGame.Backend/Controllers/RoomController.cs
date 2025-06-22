using Microsoft.AspNetCore.Mvc;
using BunkerGame.Backend.DTOs;
using BunkerGame.Backend.Services;
using BunkerGame.Backend.Responses;

namespace BunkerGame.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;
    private readonly VotingService _votingService;

    public RoomController(RoomService roomService, VotingService votingService)
    {
        _roomService = roomService;
        _votingService = votingService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom(CreateRoomDto request)
    {
        try
        {
            var room = await _roomService.CreateRoomAsync(request, request.Nickname);
            
            var response = new ApiResponse<object>(
                success: true,
                message: "Комната успешно создана!",
                data: new { 
                    RoomId = room.Id, 
                    SessionCode = room.SessionCode,
                    MaxPlayers = room.MaxPlayers,
                    WinnersCount = room.WinnersCount
                }
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> JoinRoom(JoinRoomDto request)
    {
        try
        {
            var roomPlayer = await _roomService.JoinRoomByCodeAsync(request.SessionCode, request.Nickname);
            
            if (roomPlayer == null)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Не удалось присоединиться к комнате. Проверьте код сессии и никнейм.",
                    data: null
                );
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<object>(
                success: true,
                message: "Успешно присоединились к комнате!",
                data: new { RoomId = roomPlayer.GameRoomId, RoomPlayerId = roomPlayer.Id }
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetRoomInfo(int roomId)
    {
        try
        {
            var roomInfo = await _roomService.GetRoomInfoAsync(roomId);
            
            if (roomInfo == null)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Комната не найдена.",
                    data: null
                );
                return NotFound(errorResponse);
            }

            var response = new ApiResponse<RoomInfoDto>(
                success: true,
                message: "Информация о комнате получена.",
                data: roomInfo
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> StartGame([FromBody] StartGameRequest request)
    {
        try
        {
            var success = await _roomService.StartGameAsync(request.RoomId);
            
            if (!success)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Не удалось начать игру. Убедитесь, что в комнате минимум 5 игроков.",
                    data: null
                );
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<object>(
                success: true,
                message: "Игра началась!",
                data: null
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> StartVoting([FromBody] StartVotingRequest request)
    {
        try
        {
            var votingRound = await _votingService.StartVotingRoundAsync(request.RoomId);
            
            if (votingRound == null)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Не удалось начать голосование.",
                    data: null
                );
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<object>(
                success: true,
                message: $"Раунд голосования #{votingRound.RoundNumber} начался!",
                data: new { VotingRoundId = votingRound.Id, RoundNumber = votingRound.RoundNumber }
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Vote([FromBody] VoteRequest request)
    {
        try
        {
            var success = await _votingService.VoteAsync(request.VotingRoundId, request.VoterId, request.VotedForId);
            
            if (!success)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Не удалось проголосовать.",
                    data: null
                );
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<object>(
                success: true,
                message: "Голос учтен!",
                data: null
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentVoting(int roomId)
    {
        try
        {
            var votingRound = await _votingService.GetCurrentVotingRoundAsync(roomId);
            
            if (votingRound == null)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Активное голосование не найдено.",
                    data: null
                );
                return NotFound(errorResponse);
            }

            var response = new ApiResponse<VotingRoundDto>(
                success: true,
                message: "Информация о голосовании получена.",
                data: votingRound
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> RevealCharacteristic([FromBody] RevealCharacteristicRequest request)
    {
        try
        {
            var success = await _roomService.RevealCharacteristicAsync(request.RoomPlayerId, request.Characteristic);
            
            if (!success)
            {
                var errorResponse = new ApiResponse<object>(
                    success: false,
                    message: "Не удалось открыть характеристику.",
                    data: null
                );
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<object>(
                success: true,
                message: "Характеристика открыта!",
                data: null
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<object>(
                success: false,
                message: $"Ошибка: {ex.Message}",
                data: null
            );

            return StatusCode(500, errorResponse);
        }
    }
}

// Request DTOs
public class StartGameRequest
{
    public int RoomId { get; set; }
}

public class StartVotingRequest
{
    public int RoomId { get; set; }
}

public class VoteRequest
{
    public int VotingRoundId { get; set; }
    public int VoterId { get; set; }
    public int VotedForId { get; set; }
}

public class RevealCharacteristicRequest
{
    public int RoomPlayerId { get; set; }
    public string Characteristic { get; set; } = default!;
}