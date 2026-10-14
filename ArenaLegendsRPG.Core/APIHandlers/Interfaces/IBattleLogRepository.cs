using ArenaLegendsRPG.Core.APIHandlers.DTOs;

namespace ArenaLegendsRPG.Core.APIHandlers;

public interface IBattleLogRepository
{
    Task<List<BattleLogDto>> GetBattleLogsAsync(int characterId);
    Task SaveBattleLogAsync(int characterId, BattleLogDto battleLog);

}