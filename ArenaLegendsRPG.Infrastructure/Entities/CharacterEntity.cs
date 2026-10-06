namespace ArenaLegendsRPG.Infrastructure.Entities;

public class CharacterEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int BaseAttackDamage { get; set; }
    public int BaseMagicDamage { get; set; }
    public int BaseAttackResist { get; set; }
    public int BaseMagicResist { get; set; }
    public List<ItemEntity> Items { get; set; } = new();
}