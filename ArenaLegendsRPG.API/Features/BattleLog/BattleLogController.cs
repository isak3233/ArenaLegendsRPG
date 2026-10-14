using ArenaLegendsRPG.API.Features.Character.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ArenaLegendsRPG.API.Features.BattleLog;

[ApiController]
[Route("api/battlelog")]
public class BattleLogController : ControllerBase
{
    private readonly IBattleLogService _battleLogService;
    public BattleLogController(IBattleLogService battleLogService)
    {
        _battleLogService = battleLogService;
    }
    [HttpGet("{characterId:int}")]
    public IActionResult GetBattleLogs(int characterId)
    {
        //var battleLogs = _battleLogService.GetBattleLogs(characterId);
        return Ok();
    }
}