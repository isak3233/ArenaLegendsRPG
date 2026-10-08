using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices.Results;
using ArenaLegendsRPG.Core.Monster;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface ICombatService
{
    CombatRoundResult ProcessPlayerAttack(Character player, IMonster monster);
    FleeResult AttemptFlee();
}