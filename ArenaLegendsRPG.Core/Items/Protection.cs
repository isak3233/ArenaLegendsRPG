

namespace ArenaLegendsRPG.Core.Items;

public class Protection : Item
{
    public int? AttackResist { get; }
    public int? MagicResist { get; }

    public Protection(string name, string description, int attackResist, int magicResist) : base(name, description)
    {
        AttackResist = attackResist;
        MagicResist = magicResist;
    }
}