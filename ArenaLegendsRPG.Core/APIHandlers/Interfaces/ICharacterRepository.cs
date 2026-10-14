using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.APIHandlers;

public interface ICharacterRepository
{
    Task<int> SaveAsync(Character character);
    Task<Character?> LoadAsync(int id);
}