namespace ArenaLegendsRPG.API.Features.Character.DTOs;

public class CharacterResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Level { get; set; } = 1;
    public int Xp { get; set; }
    public int BaseAttackDamage { get; set; }
    public int BaseMagicDamage { get; set; }
    public int BaseAttackResist { get; set; }
    public int BaseMagicResist { get; set; }
    public WeaponDto? EquippedWeapon { get; set; }
    public ProtectionDto? EquippedProtection { get; set; }
    public List<ItemDto> Items { get; set; } = new();
}