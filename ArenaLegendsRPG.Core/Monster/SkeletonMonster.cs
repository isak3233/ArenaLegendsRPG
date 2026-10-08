namespace ArenaLegendsRPG.Core.Monster;

public class SkeletonMonster : MonsterBase
{
    public SkeletonMonster(int maxHealth = 30, int attackResist = 2, int magicResist = 0, int attackDamage = 2, int magicDamage = 8)
        : base(maxHealth, attackResist, magicResist, attackDamage, magicDamage)
    {
    }
}