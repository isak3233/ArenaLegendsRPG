namespace ArenaLegendsRPG.API.Features.Character.DTOs;

public class WeaponDto : ItemDto
{
    public int AttackBonus { get; init; }
    public int MagicBonus { get; init; }
}