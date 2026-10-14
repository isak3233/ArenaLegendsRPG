
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ArenaLegendsRPG.API;
using ArenaLegendsRPG.API.Features.BattleLog.DTOs;

namespace ArenaLegendsRPG.IntegrationTests.ApiTests;

public class BattleLogApiTests
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string Url = "/api/battlelog";
    private const string CharacterUrl = "/api/characters";

    private readonly HttpClient _client;

    public BattleLogApiTests(
        CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private static BattleLogDto CreateBattleLog(
        string monsterName = "Goblin",
        bool isPlayerWinner = true) => new()
        {
            MonsterName = monsterName,
            MonsterHealth = 150,
            MonsterAttackResist = 10,
            MonsterMagicResist = 5,
            MonsterAttackDamage = 20,
            MonsterMagicDamage = 15,
            DamageDoneByPlayer = 150,
            DamageDoneByMonster = 40,
            IsPlayerWinner = isPlayerWinner,
            BattleFinished = new DateTime(
            2026, 10, 14, 12, 30, 0, DateTimeKind.Utc)
        };

    private async Task<int> CreateCharacterAsync(
        string name = "Hero")
    {
        var request = new
        {
            Name = name,
            MaxHealth = 100,
            Health = 80,
            Level = 2,
            Xp = 50,
            BaseAttackDamage = 10,
            BaseMagicDamage = 1,
            BaseAttackResist = 5,
            BaseMagicResist = 2,
            Items = Array.Empty<object>()
        };

        var response = await _client.PostAsJsonAsync(
            CharacterUrl, request);

        response.EnsureSuccessStatusCode();

        using var document = JsonDocument.Parse(
            await response.Content.ReadAsStringAsync());

        return document.RootElement
            .GetProperty("savedId")
            .GetInt32();
    }

    private async Task<List<BattleLogDto>> GetBattleLogsAsync(
        int characterId)
    {
        var response = await _client.GetAsync(
            $"{Url}/{characterId}");

        var json = await response.Content.ReadAsStringAsync();

        Assert.True(
            response.IsSuccessStatusCode,
            $"Status: {(int)response.StatusCode}\nSvar: {json}");

        Console.WriteLine($"BattleLog API svar: {json}");

        var battleLogs =
            await response.Content.ReadFromJsonAsync<List<BattleLogDto>>();

        return battleLogs!;
    }

    private async Task<HttpResponseMessage> SaveBattleLogAsync(
        int characterId,
        BattleLogDto battleLog)
    {
        return await _client.PostAsJsonAsync(
            $"{Url}/{characterId}", battleLog);
    }

    [Fact]
    public async Task Post_ValidBattleLog_ReturnsCreated()
    {
        var characterId = await CreateCharacterAsync();

        var response = await SaveBattleLogAsync(
            characterId, CreateBattleLog());

        Assert.Equal(
            HttpStatusCode.Created,
            response.StatusCode);
    }

    [Fact]
    public async Task Post_ValidBattleLog_CanBeRetrieved()
    {
        var characterId = await CreateCharacterAsync();
        var battleLog = CreateBattleLog();

        var postResponse = await SaveBattleLogAsync(
            characterId, battleLog);

        Assert.Equal(
            HttpStatusCode.Created,
            postResponse.StatusCode);

        var battleLogs =
            await GetBattleLogsAsync(characterId);

        var savedLog = Assert.Single(battleLogs);

        Assert.Equal(
            battleLog.MonsterName, savedLog.MonsterName);
        Assert.Equal(
            battleLog.MonsterHealth, savedLog.MonsterHealth);
        Assert.Equal(
            battleLog.MonsterAttackResist,
            savedLog.MonsterAttackResist);
        Assert.Equal(
            battleLog.MonsterMagicResist,
            savedLog.MonsterMagicResist);
        Assert.Equal(
            battleLog.MonsterAttackDamage,
            savedLog.MonsterAttackDamage);
        Assert.Equal(
            battleLog.MonsterMagicDamage,
            savedLog.MonsterMagicDamage);
        Assert.Equal(
            battleLog.DamageDoneByPlayer,
            savedLog.DamageDoneByPlayer);
        Assert.Equal(
            battleLog.DamageDoneByMonster,
            savedLog.DamageDoneByMonster);
        Assert.Equal(
            battleLog.IsPlayerWinner,
            savedLog.IsPlayerWinner);
        Assert.Equal(
            battleLog.BattleFinished,
            savedLog.BattleFinished);
    }

    [Fact]
    public async Task Post_TwoBattleLogs_ReturnsBothLogs()
    {
        var characterId = await CreateCharacterAsync();

        var firstLog = CreateBattleLog("Dragon");
        var secondLog = CreateBattleLog("Goblin", false);

        var firstResponse = await SaveBattleLogAsync(
            characterId, firstLog);

        var secondResponse = await SaveBattleLogAsync(
            characterId, secondLog);

        Assert.Equal(
            HttpStatusCode.Created,
            firstResponse.StatusCode);
        Assert.Equal(
            HttpStatusCode.Created,
            secondResponse.StatusCode);

        var battleLogs =
            await GetBattleLogsAsync(characterId);

        Assert.Equal(2, battleLogs.Count);
        Assert.Contains(
            battleLogs,
            log => log.MonsterName == "Dragon");
        Assert.Contains(
            battleLogs,
            log => log.MonsterName == "Goblin");
    }

    [Fact]
    public async Task Get_BattleLogs_ReturnsCorrectWinnerStatus()
    {
        var characterId = await CreateCharacterAsync();

        await SaveBattleLogAsync(
            characterId,
            CreateBattleLog("Dragon", true));

        await SaveBattleLogAsync(
            characterId,
            CreateBattleLog("Goblin", false));

        var battleLogs =
            await GetBattleLogsAsync(characterId);

        Assert.Contains(
            battleLogs,
            log => log.MonsterName == "Dragon"
                && log.IsPlayerWinner);

        Assert.Contains(
            battleLogs,
            log => log.MonsterName == "Goblin"
                && !log.IsPlayerWinner);
    }

    [Fact]
    public async Task Get_TwoCharacters_ReturnsOnlyTheirOwnBattleLogs()
    {
        var firstCharacterId =
            await CreateCharacterAsync("FirstHero");

        var secondCharacterId =
            await CreateCharacterAsync("SecondHero");

        await SaveBattleLogAsync(
            firstCharacterId,
            CreateBattleLog("Dragon"));

        await SaveBattleLogAsync(
            secondCharacterId,
            CreateBattleLog("Goblin"));

        var firstLogs =
            await GetBattleLogsAsync(firstCharacterId);

        var secondLogs =
            await GetBattleLogsAsync(secondCharacterId);

        var firstLog = Assert.Single(firstLogs);
        var secondLog = Assert.Single(secondLogs);

        Assert.Equal("Dragon", firstLog.MonsterName);
        Assert.Equal("Goblin", secondLog.MonsterName);
    }

    [Fact]
    public async Task Get_CharacterWithoutBattleLogs_ReturnsEmptyList()
    {
        var characterId = await CreateCharacterAsync();

        var battleLogs =
            await GetBattleLogsAsync(characterId);

        Assert.Empty(battleLogs);
    }
}
