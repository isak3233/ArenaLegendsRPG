using System.Globalization;
using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.GameServices.Results;

namespace ArenaLegendsRPG.Core.GameServices;

public class CharacterCreationService : ICharacterCreationService
{
    private const int MaxNameLength = 20;
    private readonly string _filePath;
    private readonly ICharacterRepository _characterRepository;

    public CharacterCreationService(ICharacterRepository characterRepository, string? filePath = null)
    {
        _filePath = Path.Combine(AppContext.BaseDirectory, "characterids.txt");
        if (filePath != null) _filePath = filePath;


        _characterRepository = characterRepository;
    }

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
            level: 1,
            xp: 0,
            baseAttackDamage: 10,
            baseMagicDamage: 5,
            baseAttackResist: 5,
            baseMagicResist: 2);
    }

    public async Task<SaveCharacterResult> SaveAsync(Character character)
    {
        try
        {
            var savedId = await _characterRepository.SaveAsync(character);

            var directory = Path.GetDirectoryName(Path.GetFullPath(_filePath));
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }


            await File.AppendAllTextAsync(_filePath, savedId.ToString(CultureInfo.InvariantCulture) + Environment.NewLine);

            return new SaveCharacterResult(true);
        }
        catch (Exception e)
        {
            return new SaveCharacterResult(false, e.Message);
        }


    }


    public async Task<List<LoadedCharacterResult>> LoadAllCharacters()
    {
        if (!File.Exists(_filePath)) return new List<LoadedCharacterResult>();
        var savedIds = new List<int>();

        foreach (var line in await File.ReadAllLinesAsync(_filePath))
        {
            if (int.TryParse(line.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var number))
            {
                savedIds.Add(number);
            }
        }

        var tasks = savedIds.Select(async id =>
        {
            try { return await _characterRepository.LoadAsync(id); }
            catch (HttpRequestException e) { return null; }
        }).ToList();
        var characters = await Task.WhenAll(tasks);

        var loadedCharacterResults = new List<LoadedCharacterResult>();
        for (int i = 0; i < characters.Length; i++)
        {
            var character = characters[i];
            if (character == null) continue;
            var savedId = savedIds[i];
            loadedCharacterResults.Add(new LoadedCharacterResult(character, savedId));
        }

        return loadedCharacterResults;
    }


}