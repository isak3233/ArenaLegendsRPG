using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.GameServices.Results;
using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface ICombatService
{
    CombatRoundResult ProcessPlayerAttack(
        Character player,
        IMonster monster,
        DamageType damageType);


    CombatRoundResult ProcessPlayerAttack(Character player, IMonster monster);

    void AddMonsterInfoToBattleLog(IMonster monster);

    FleeResult AttemptFlee();
}