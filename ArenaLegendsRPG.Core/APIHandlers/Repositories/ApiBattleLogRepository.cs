using System.Net.Http.Json;
using ArenaLegendsRPG.Core.APIHandlers.DTOs;

namespace ArenaLegendsRPG.Core.APIHandlers.DTOs;

public class ApiBattleLogRepository : IBattleLogRepository
{
    private const string Url = "/api/battlelog";

    private readonly HttpClient _http;

    public ApiBattleLogRepository(HttpClient http, ICharacterMapper characterMapper)
    {
        _http = http;
    }
    public async Task<List<BattleLogDto>> GetBattleLogsAsync(int characterId)
    {
        var response = await _http.GetAsync($"{Url}/{characterId}");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<List<BattleLogDto>>();
        return body!;
    }

    public async Task SaveBattleLogAsync(int characterId, BattleLogDto battleLog)
    {
        var response = await _http.PostAsJsonAsync($"{Url}/{characterId}", battleLog);
        response.EnsureSuccessStatusCode();

    }
}