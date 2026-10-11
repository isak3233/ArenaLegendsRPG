using ArenaLegendsRPG.API.Features.Character.DTOs;

namespace ArenaLegendsRPG.API.Features.Character;

public interface ICharacterService
{
    public Task<int> SaveAsync(SaveCharacterRequest request);
    public Task<CharacterResponse?> GetAsync(int id);
}