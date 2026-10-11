using ArenaLegendsRPG.API.Features.Character.DTOs;
using ArenaLegendsRPG.Infrastructure.Data;
using ArenaLegendsRPG.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArenaLegendsRPG.API.Features.Character;

public class CharacterService : ICharacterService
{
    private readonly ArenaLegendsDbContext _dbContext;

    public CharacterService(ArenaLegendsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveAsync(SaveCharacterRequest request)
    {
        var equippedWeapons = request.Items.OfType<WeaponDto>().Count(w => w.IsEquipped);
        var equippedProtections = request.Items.OfType<ProtectionDto>().Count(p => p.IsEquipped);

        if (equippedWeapons > 1 || equippedProtections > 1)
        {
            throw new ArgumentException("Only one weapon and protection is allowed to be equipped.");
        }

        if (request.Name.Length < 1)
        {
            throw new ArgumentException("Name is required.");
        }


        var characterEntity = new CharacterEntity
        {
            Name = request.Name,
            MaxHealth = request.MaxHealth,
            Health = request.Health,
            Level = request.Level,
            Xp = request.Xp,
            BaseAttackDamage = request.BaseAttackDamage,
            BaseMagicDamage = request.BaseMagicDamage,
            BaseAttackResist = request.BaseAttackResist,
            BaseMagicResist = request.BaseMagicResist,
            Items = request.Items.Select(ToEntity).ToList()
        };
        _dbContext.Characters.Add(characterEntity);
        await _dbContext.SaveChangesAsync();

        return characterEntity.Id;
    }

    public async Task<CharacterResponse?> GetAsync(int id)
    {
        var character = await _dbContext.Characters
            .AsNoTracking()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (character == null)
        {
            return null;
        }
        var items = character.Items.Select(ToDto).ToList();

        return new CharacterResponse
        {
            Id = character.Id,
            Name = character.Name,
            Health = character.Health,
            MaxHealth = character.MaxHealth,
            Level = character.Level,
            Xp = character.Xp,
            BaseAttackDamage = character.BaseAttackDamage,
            BaseMagicDamage = character.BaseMagicDamage,
            BaseAttackResist = character.BaseAttackResist,
            BaseMagicResist = character.BaseMagicResist,
            Items = items,
            EquippedWeapon = items.OfType<WeaponDto>().FirstOrDefault(w => w.IsEquipped),
            EquippedProtection = items.OfType<ProtectionDto>().FirstOrDefault(p => p.IsEquipped)
        };
    }

    private static ItemEntity ToEntity(ItemDto item) => item switch
    {
        WeaponDto w => new WeaponEntity
        {
            Name = w.Name,
            Description = w.Description,
            IsEquipped = w.IsEquipped,
            AttackBonus = w.AttackBonus,
            MagicBonus = w.MagicBonus
        },
        ProtectionDto p => new ProtectionEntity
        {
            Name = p.Name,
            Description = p.Description,
            IsEquipped = p.IsEquipped,
            AttackResist = p.AttackResist,
            MagicResist = p.MagicResist
        },
        _ => throw new InvalidOperationException($"Unknown item-type: {item.GetType().Name}")
    };
    private static ItemDto ToDto(ItemEntity item) => item switch
    {
        WeaponEntity w => new WeaponDto
        {
            Name = w.Name,
            Description = w.Description,
            IsEquipped = w.IsEquipped,
            AttackBonus = w.AttackBonus,
            MagicBonus = w.MagicBonus
        },
        ProtectionEntity p => new ProtectionDto
        {
            Name = p.Name,
            Description = p.Description,
            IsEquipped = p.IsEquipped,
            AttackResist = p.AttackResist,
            MagicResist = p.MagicResist
        },
        _ => throw new InvalidOperationException($"Unknown item-type: {item.GetType().Name}")
    };
}