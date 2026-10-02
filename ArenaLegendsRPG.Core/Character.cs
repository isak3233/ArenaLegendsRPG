using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.Items;


namespace ArenaLegendsRPG.Core;

//TODO: Ska Character använda IMonster eller ska de båda använda ett gemensamt
//ICombatant så att combat koden kan hantera characters och monsters på samma sätt?
//Ska inventory ha begränsningar utifrån specifika items i en specifik karaktärs inventory? 
//tex bara ett av samma vapen?

public class Character
{
    public string Name { get; }
    public int Health { get; private set; }

    public int BaseAttackDamage { get; }
    public int BaseMagicDamage { get; }
    public int BaseAttackResist { get; }
    public int BaseMagicResist { get; }

    public Weapon? EquippedWeapon { get; private set; }
    public Protection? EquippedProtection { get; private set; }

    public int AttackDamage => BaseAttackDamage + (EquippedWeapon?.AttackBonus ?? 0);
    public int AttackResist => BaseAttackResist + (EquippedProtection?.AttackResist ?? 0);

    public int MagicDamage => BaseMagicDamage + (EquippedWeapon?.MagicBonus ?? 0);
    public int MagicResist => BaseMagicResist + (EquippedProtection?.MagicResist ?? 0);
    public Inventory Inventory { get; } = new();



    public Character(string name, int health, int baseAttackDamage, int baseMagicDamage, int baseAttackResist, int baseMagicResist)
    {
        Name = name;
        Health = health;
        BaseAttackDamage = baseAttackDamage;
        BaseMagicDamage = baseMagicDamage;
        BaseAttackResist = baseAttackResist;
        BaseMagicResist = baseMagicResist;
    }


    public void EquipWeapon(Weapon weapon)
    {
        if (!Inventory.GetItems().Contains(weapon))
        {
            throw new InvalidOperationException("Cannot equip a weapon that is not in the inventory");
        }

        EquippedWeapon = weapon;
    }
    public void EquipProtection(Protection protection)
    {
        if (!Inventory.GetItems().Contains(protection))
        {
            throw new InvalidOperationException("Cannot equip armor that is not in the inventory");
        }
        EquippedProtection = protection;
    }

    public int TakeDamage(Damage damage)
    {
        var actualDamage = DamageCalculator.Calculate(damage, AttackResist, MagicResist);
        Health = Math.Max(0, Health - actualDamage);
        return actualDamage;
    }
}



