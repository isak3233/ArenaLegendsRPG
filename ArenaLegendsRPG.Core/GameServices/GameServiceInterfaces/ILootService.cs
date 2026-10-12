using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface ILootService
{
    IItem GenerateLoot();
    IItem? RollMonsterDrop();
}