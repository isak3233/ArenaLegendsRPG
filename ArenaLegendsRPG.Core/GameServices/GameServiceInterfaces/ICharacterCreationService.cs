using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

public interface ICharacterCreationService
{
    bool ValidateCharacterName(string name);
    Character CreateCharacter(string name);
}