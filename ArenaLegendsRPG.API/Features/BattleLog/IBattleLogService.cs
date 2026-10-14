using ArenaLegendsRPG.API.Features.BattleLog.DTOs;

namespace ArenaLegendsRPG.API.Features.BattleLog;

public interface IBattleLogService
{
    Task<List<BattleLogDto>> GetBattleLogs(int characterId);
}