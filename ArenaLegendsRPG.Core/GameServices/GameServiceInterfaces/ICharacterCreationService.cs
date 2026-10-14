using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices.Results;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface ICharacterCreationService
{
    bool ValidateCharacterName(string name);
    Character CreateCharacter(string name);
    Task<SaveCharacterResult> SaveAsync(Character character);
    Task<List<LoadedCharacterResult>> LoadAllCharacters();
}