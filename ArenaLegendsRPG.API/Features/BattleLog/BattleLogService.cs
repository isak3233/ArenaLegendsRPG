using ArenaLegendsRPG.API.Features.BattleLog.DTOs;
using ArenaLegendsRPG.Infrastructure.Data;
using ArenaLegendsRPG.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArenaLegendsRPG.API.Features.BattleLog;

public class BattleLogService : IBattleLogService
{
    private readonly ArenaLegendsDbContext _dbContext;

    public BattleLogService(ArenaLegendsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BattleLogDto>> GetBattleLogs(int characterId)
    {
        var battleLogs = await _dbContext.BattleLogs
            .Where(x => x.CharacterId == characterId)
            .ToListAsync();
        var battleLogsDto = battleLogs.Select(battleLog => new BattleLogDto
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
        })
        .ToList();
        return battleLogsDto;
    }

    public async Task SaveBattleLog(int characterId, BattleLogDto battleLogDto)
    {
        var battleLogEntity = new BattleLogEntity
        {
            CharacterId = characterId,
            MonsterName = battleLogDto.MonsterName,
            MonsterHealth = battleLogDto.MonsterHealth,
            MonsterAttackResist = battleLogDto.MonsterAttackResist,
            MonsterMagicResist = battleLogDto.MonsterMagicResist,
            MonsterAttackDamage = battleLogDto.MonsterAttackDamage,
            MonsterMagicDamage = battleLogDto.MonsterMagicDamage,
            DamageDoneByPlayer = battleLogDto.DamageDoneByPlayer,
            DamageDoneByMonster = battleLogDto.DamageDoneByMonster,
            IsPlayerWinner = battleLogDto.IsPlayerWinner,
            BattleFinished = battleLogDto.BattleFinished,
        };
        _dbContext.BattleLogs.Add(battleLogEntity);
        await _dbContext.SaveChangesAsync();
    }
}