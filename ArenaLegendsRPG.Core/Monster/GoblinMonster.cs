namespace ArenaLegendsRPG.Core.Monster;

public class GoblinMonster : MonsterBase
{
    public GoblinMonster(int maxHealth = 30, int attackResist = 2, int magicResist = 0, int attackDamage = 8, int magicDamage = 2 ) 
        : base(maxHealth, attackResist, magicResist, attackDamage, magicDamage)
    {
    }
}