
internal class TestMonster : MonsterBase
{
    public TestMonster(int maxHealth = 30, int attackResist = 2, int magicResist = 0, int attackDamage = 5, int magicDamage = 0)
        : base(maxHealth, attackResist, magicResist, attackDamage, magicDamage)
    {
    }
}