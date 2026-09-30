using System;
using System.Collections.Generic;
using System.Text;
using ArenaLegendsRPG.Core.Items;
namespace ArenaLegendsRPG.Core.Items;

public class Weapon : Item
{
    public int AttackBonus { get; }

    public Weapon(string name, string description, int attackBonus)
        : base(name, description)
    {
        AttackBonus = attackBonus;
    }
}
