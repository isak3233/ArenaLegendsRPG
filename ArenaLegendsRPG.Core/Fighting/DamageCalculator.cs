namespace ArenaLegendsRPG.Core.Fighting;

public static class DamageCalculator
{
    public static int Calculate(Damage damage, int attackResist, int magicResist)
    {
        int reduction;
        switch (damage.Type)
        {
            case (DamageType.Physical):
                reduction = attackResist;
                break;
            case (DamageType.Magic):
                reduction = magicResist;
                break;
            default:
                reduction = 0;
                break;
        }
        return Math.Max(0, damage.Amount - reduction);
    }
}