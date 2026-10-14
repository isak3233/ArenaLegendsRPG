using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.GameServices.Results;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubCharacterCreationService : ICharacterCreationService
{
    public bool NameIsValid { get; set; } = true;
    public IReadOnlyList<LoadedCharacterResult> Loaded { get; set; } = Array.Empty<LoadedCharacterResult>();
    public Character? LoadResult { get; set; } = new("Loaded", 100, 1, 0, 10, 0, 5, 2);

    public bool ValidateCharacterName(string name) => NameIsValid;
    public Character CreateCharacter(string name) => new(name, 100, 1, 0, 10, 0, 5, 2);
    public Task<List<LoadedCharacterResult>> LoadAllCharacters() => Task.FromResult(Loaded.ToList());
    public Task<Character?> LoadAsync(int id) => Task.FromResult(LoadResult);
    public Task<SaveCharacterResult> SaveAsync(Character character) => Task.FromResult(new SaveCharacterResult(true));
}