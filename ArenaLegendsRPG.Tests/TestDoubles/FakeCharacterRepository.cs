using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class FakeCharacterRepository : ICharacterRepository
{
    private readonly Dictionary<int, Character> _stored = new();
    private readonly HashSet<int> _failingIds = new();
    private int _nextId = 1;

    public Exception? FailWith { get; init; }

    public void Add(int id, Character character) => _stored[id] = character;
    public void FailLoadingOf(int id) => _failingIds.Add(id);

    public IReadOnlyCollection<Character> SavedCharacters => _stored.Values;

    public Task<int> SaveAsync(Character character)
    {
        if (FailWith is not null) throw FailWith;

        var id = _nextId++;
        _stored[id] = character;
        return Task.FromResult(id);
    }

    public Task<Character?> LoadAsync(int id)
    {
        if (_failingIds.Contains(id)) throw new HttpRequestException();
        return Task.FromResult(_stored.GetValueOrDefault(id));
    }
}