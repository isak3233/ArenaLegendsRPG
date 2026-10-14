using ArenaLegendsRPG.Core.APIHandlers.DTOs;
using ArenaLegendsRPG.Core.Fighting;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface IBattleLogService
{
    Task<List<BattleLog>> GetBattleLogs(int characterId);
    Task SaveBattleLog(int characterId, BattleLog battleLog);
}