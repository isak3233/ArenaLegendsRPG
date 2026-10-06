using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.GameServices;

public interface ICharacterCreationService
{
    Character CreateCharacter(string name);
}

public class CharacterCreationService : ICharacterCreationService
{
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