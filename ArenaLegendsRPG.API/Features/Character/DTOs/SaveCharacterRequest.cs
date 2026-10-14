namespace ArenaLegendsRPG.API.Features.Character.DTOs;

public class SaveCharacterRequest
{
    public int? SavedId { get; init; }
    public string Name { get; init; } = string.Empty;
    public int MaxHealth { get; init; }
    public int Health { get; init; }
    public int Level { get; init; }
    public int Xp { get; init; }
    public int BaseAttackDamage { get; init; }
    public int BaseMagicDamage { get; init; }
    public int BaseAttackResist { get; init; }
    public int BaseMagicResist { get; init; }

    public List<ItemDto> Items { get; init; } = new();
}