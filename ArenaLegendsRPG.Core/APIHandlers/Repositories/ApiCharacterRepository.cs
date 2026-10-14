using System.Net;
using System.Net.Http.Json;
using ArenaLegendsRPG.Core.APIHandlers.DTOs;
using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.Items;
using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.APIHandlers;

public class ApiCharacterRepository : ICharacterRepository
{
    private const string Url = "/api/characters";

    private readonly HttpClient _http;
    private readonly ICharacterMapper _characterMapper;

    public ApiCharacterRepository(HttpClient http, ICharacterMapper characterMapper)
    {
        _http = http;
        _characterMapper = characterMapper;
    }

    public async Task<int> SaveAsync(Character character)
    {
        var request = _characterMapper.ToRequest(character);

        var response = await _http.PostAsJsonAsync(Url, request);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<SaveCharacterResponse>();
        return body!.SavedId;
    }

    public async Task<Character?> LoadAsync(int id)
    {
        var response = await _http.GetAsync($"{Url}/{id}");


        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<CharacterResponse>();
        return _characterMapper.ToCharacter(dto!);
    }

}