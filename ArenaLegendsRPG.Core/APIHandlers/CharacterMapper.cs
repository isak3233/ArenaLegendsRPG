using ArenaLegendsRPG.Core.APIHandlers.DTOs;
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.APIHandlers;

public class CharacterMapper : ICharacterMapper
{
    public SaveCharacterRequest ToRequest(Character character)
    {
        return new SaveCharacterRequest
        {
            SavedId = character.SavedPlayerId,
            Name = character.Name,
            MaxHealth = character.MaxHealth,
            Health = character.Health,
            Level = character.Level,
            Xp = character.Xp,
            BaseAttackDamage = character.BaseAttackDamage,
            BaseMagicDamage = character.BaseMagicDamage,
            BaseAttackResist = character.BaseAttackResist,
            BaseMagicResist = character.BaseMagicResist,
            Items = character.Inventory.GetItems()
                .Select(item => ToItemDto(item, IsEquipped(character, item)))
                .ToList()
        };
    }
    public Character ToCharacter(CharacterResponse response)
    {
        var character = new Character(
            name: response.Name,
            health: response.Health,
            level: response.Level,
            xp: response.Xp,
            baseAttackDamage: response.BaseAttackDamage,
            baseMagicDamage: response.BaseMagicDamage,
            baseAttackResist: response.BaseAttackResist,
            baseMagicResist: response.BaseMagicResist);

        foreach (var dto in response.Items)
        {
            var item = ToItem(dto);
            character.Inventory.AddItem(item);

            if (!dto.IsEquipped) continue;

            switch (item)
            {
                case Weapon w: character.EquipWeapon(w); break;
                case Protection p: character.EquipProtection(p); break;
            }
        }

        return character;
    }
    private bool IsEquipped(Character character, IItem item) =>
        ReferenceEquals(item, character.EquippedWeapon) ||
        ReferenceEquals(item, character.EquippedProtection);

    private ItemDto ToItemDto(IItem item, bool isEquipped) => item switch
    {
        Weapon w => new WeaponDto
        {
            Name = w.Name,
            Description = w.Description,
            IsEquipped = isEquipped,
            AttackBonus = w.AttackBonus,
            MagicBonus = w.MagicBonus
        },
        Protection p => new ProtectionDto
        {
            Name = p.Name,
            Description = p.Description,
            IsEquipped = isEquipped,
            AttackResist = p.AttackResist,
            MagicResist = p.MagicResist
        },
        _ => throw new InvalidOperationException($"Unknown item type: {item.GetType().Name}")
    };


    private Item ToItem(ItemDto dto) => dto switch
    {
        WeaponDto w => new Weapon(w.Name, w.Description, w.AttackBonus, w.MagicBonus),
        ProtectionDto p => new Protection(p.Name, p.Description, p.AttackResist, p.MagicResist),
        _ => throw new InvalidOperationException($"Unknown item type: {dto.GetType().Name}")
    };
}