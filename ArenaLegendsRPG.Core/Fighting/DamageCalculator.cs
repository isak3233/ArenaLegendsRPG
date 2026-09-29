namespace ArenaLegendsRPG.Core.Fighting;

public static class DamageCalculator
{
    public static int Calculate(Damage damage, int armor, int magicResistance)
    {
        var reduction = damage.Type switch
        {
            DamageType.Physical => armor,
            DamageType.Magic => magicResistance,
            _ => 0
        };

        return Math.Max(0, damage.Amount - reduction);
    }
}