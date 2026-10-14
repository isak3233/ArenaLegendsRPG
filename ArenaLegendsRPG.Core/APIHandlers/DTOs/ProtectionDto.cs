namespace ArenaLegendsRPG.API.Features.Character.DTOs;

public class ProtectionDto : ItemDto
{
    public int AttackResist { get; init; }
    public int MagicResist { get; init; }
}