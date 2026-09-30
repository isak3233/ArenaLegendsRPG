namespace ArenaLegendsRPG.Core.Fighting;

public static class DamageCalculator
{
    public static int Calculate(Damage damage, int armor, int magicResistance)
    {
        int reduction;
        switch (damage.Type)
        {
            case(DamageType.Physical):
                reduction = armor;
                break;
            case(DamageType.Magic):
                reduction = magicResistance;
                break;
            default:
                reduction = 0;
                break;
        }
        return Math.Max(0, damage.Amount - reduction);
    }
}