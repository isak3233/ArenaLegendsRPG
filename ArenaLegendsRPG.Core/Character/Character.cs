using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using ArenaLegendsRPG.Core.Fighting;
using ArenaLegendsRPG.Core.Items;


namespace ArenaLegendsRPG.Core.Character;

//TODO: Ska Character använda IMonster eller ska de båda använda ett gemensamt
//ICombatant så att combat koden kan hantera characters och monsters på samma sätt?
//Ska inventory ha begränsningar utifrån specifika items i en specifik karaktärs inventory? 
//tex bara ett av samma vapen?

public class Character
{
    public string Name { get; }
    public int Health { get; private set; }

    public int BaseAttackDamage { get; }
    public int BaseArmorDefence { get; }
    public int BaseMagicResistance { get; }

    public Weapon? EquippedWeapon { get; private set; }
    public Armor? EquippedArmor { get; private set; }

    public int AttackDamage => BaseAttackDamage + (EquippedWeapon?.AttackBonus ?? 0);
    public int ArmorDefence => BaseArmorDefence + (EquippedArmor?.ArmorBonus ?? 0);
    public int MagicResistance => BaseMagicResistance;
    public Inventory Inventory { get; } = new();



    public Character(string name, int health, int baseAttackDamage, int baseArmorDefence, int baseMagicResistance)
    {
        Name = name;
        Health = health;
        BaseAttackDamage = baseAttackDamage;
        BaseArmorDefence = baseArmorDefence;
        BaseMagicResistance = baseMagicResistance;
    }


    public void EquipWeapon(Weapon weapon)
    {
        if (!Inventory.Items.Contains(weapon))
            throw new InvalidOperationException("Cannot equip a weapon that is not in the inventory");
        EquippedWeapon = weapon;
    }
    public void EquipArmor(Armor armor)
    {
        if (!Inventory.Items.Contains(armor))
            throw new InvalidOperationException("Cannot equip armor that is not in the inventory");
        EquippedArmor = armor;
    }

    public int TakeDamage(Damage damage)
    {
        var actualDamage = DamageCalculator.Calculate(damage, ArmorDefence, MagicResistance);
        Health = Math.Max(0, Health - actualDamage);
        return actualDamage;
    }

}



