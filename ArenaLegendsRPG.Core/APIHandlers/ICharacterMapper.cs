using ArenaLegendsRPG.API.Features.Character.DTOs;
using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.APIHandlers;

public interface ICharacterMapper
{
    SaveCharacterRequest ToRequest(Character character);
    Character ToCharacter(CharacterResponse response);
}