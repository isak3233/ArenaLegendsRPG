using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Tests.TestDoubles;

public class StubBattleLogService : IBattleLogService
{
    public async Task<List<BattleLog>> GetBattleLogs(int characterId)
    {
        return await Task.FromResult(new List<BattleLog>());
    }

    public async Task SaveBattleLog(int characterId, BattleLog battleLog)
    {
        await Task.CompletedTask;
    }
}