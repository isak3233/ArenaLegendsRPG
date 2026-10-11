namespace ArenaLegendsRPG.Infrastructure.Entities;

public class ItemEntity
{
    public int Id { get; set; }

    public int CharacterId { get; set; }
    public CharacterEntity Character { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsEquipped { get; set; }
}