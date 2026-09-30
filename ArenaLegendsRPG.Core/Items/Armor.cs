using System;
using System.Collections.Generic;
using System.Text;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Core.Items;

public class Armor : Item
{
    public int ArmorBonus { get; }

    public Armor(string name, string description, int armorBonus)
        : base(name, description)
    {
        ArmorBonus = armorBonus;
    }
}