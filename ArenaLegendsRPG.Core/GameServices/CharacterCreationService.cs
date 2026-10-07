using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameServices;

public class CharacterCreationService : ICharacterCreationService
{
    private const int MaxNameLength = 20;

    public bool ValidateCharacterName(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
        {
            return false;
        }

        return true;
    }
    public Character CreateCharacter(string name)
    {
        return new Character(
            name: name,
            health: 100,
            baseAttackDamage: 10,
            baseMagicDamage: 5,
            baseAttackResist: 5,
            baseMagicResist: 2);
    }
}