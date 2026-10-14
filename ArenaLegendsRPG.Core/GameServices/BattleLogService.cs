using ArenaLegendsRPG.Core.APIHandlers.DTOs;
using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.APIHandlers.DTOs;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameServices;

public class BattleLogService : IBattleLogService
{
    private readonly IBattleLogRepository _battleLogRepository;
    public BattleLogService(IBattleLogRepository battleLogRepository)
    {
        _battleLogRepository = battleLogRepository;

    }
    public async Task<List<BattleLog>> GetBattleLogs(int characterId)
    {
        var battleLogDtos = await _battleLogRepository.GetBattleLogsAsync(characterId);
        var battleLogs = battleLogDtos.Select(battleLog => new BattleLog
        {
            MonsterName = battleLog.MonsterName,
            MonsterHealth = battleLog.MonsterHealth,
            MonsterAttackResist = battleLog.MonsterAttackResist,
            MonsterMagicResist = battleLog.MonsterMagicResist,
            MonsterAttackDamage = battleLog.MonsterAttackDamage,
            MonsterMagicDamage = battleLog.MonsterMagicDamage,
            DamageDoneByPlayer = battleLog.DamageDoneByPlayer,
            DamageDoneByMonster = battleLog.DamageDoneByMonster,
            IsPlayerWinner = battleLog.IsPlayerWinner,
            BattleFinished = battleLog.BattleFinished,
        });
        return battleLogs.ToList();
    }

    public async Task SaveBattleLog(int characterId, BattleLog battleLog)
    {
        var battleLogDto = new BattleLogDto
        {
            MonsterName = battleLog.MonsterName,
            MonsterHealth = battleLog.MonsterHealth,
            MonsterAttackResist = battleLog.MonsterAttackResist,
            MonsterMagicResist = battleLog.MonsterMagicResist,
            MonsterAttackDamage = battleLog.MonsterAttackDamage,
            MonsterMagicDamage = battleLog.MonsterMagicDamage,
            DamageDoneByPlayer = battleLog.DamageDoneByPlayer,
            DamageDoneByMonster = battleLog.DamageDoneByMonster,
            IsPlayerWinner = battleLog.IsPlayerWinner,
            BattleFinished = battleLog.BattleFinished,
        };
        await _battleLogRepository.SaveBattleLogAsync(characterId, battleLogDto);
    }

}