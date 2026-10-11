using System.Net;
using System.Net.Http.Json;
using System.Text;
using ArenaLegendsRPG.API;
using ArenaLegendsRPG.API.Features.Character.DTOs;

// plus using för SaveCharacterRequest, SaveCharacterResponse, CharacterResponse,
// ItemRequest, WeaponRequest, ProtectionRequest, WeaponDto och ProtectionDto

namespace ArenaLegendsRPG.IntegrationTests.ApiTests;

public class CharacterApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string Url = "/api/characters";

    private readonly HttpClient _client;

    public CharacterApiTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    private static SaveCharacterRequest CreateRequest(string name = "Hero", List<ItemDto>? items = null) => new()
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
        Items = items ?? new List<ItemDto>()
    };

    private static WeaponDto CreateSword(bool isEquipped = false) => new()
    {
        Name = "Sword",
        Description = "A sharp blade",
        IsEquipped = isEquipped,
        AttackBonus = 5,
        MagicBonus = 3
    };

    private static ProtectionDto CreatePlate(bool isEquipped = false) => new()
    {
        Name = "Plate",
        Description = "A sturdy plate",
        IsEquipped = isEquipped,
        AttackResist = 4,
        MagicResist = 2
    };

    private async Task<int> SaveAsync(SaveCharacterRequest request)
    {
        var response = await _client.PostAsJsonAsync(Url, request);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<SaveCharacterResponse>();
        return body!.SavedId;
    }

    private async Task<CharacterResponse> GetAsync(int id)
    {
        var response = await _client.GetAsync($"{Url}/{id}");
        response.EnsureSuccessStatusCode();

        var character = await response.Content.ReadFromJsonAsync<CharacterResponse>();
        return character!;
    }


    [Fact]
    public async Task Save_ValidCharacter_ReturnsCreatedWithId()
    {
        var response = await _client.PostAsJsonAsync(Url, CreateRequest());

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<SaveCharacterResponse>();
        Assert.True(body!.SavedId > 0);
    }

    [Fact]
    public async Task Save_ValidCharacter_LocationHeaderPointsToTheNewCharacter()
    {
        var response = await _client.PostAsJsonAsync(Url, CreateRequest());
        var body = await response.Content.ReadFromJsonAsync<SaveCharacterResponse>();

        Assert.Equal($"{Url}/{body!.SavedId}", response.Headers.Location!.ToString());
    }

    [Fact]
    public async Task Save_TwoCharacters_GetDifferentIds()
    {
        var first = await SaveAsync(CreateRequest("First"));
        var second = await SaveAsync(CreateRequest("Second"));

        Assert.NotEqual(first, second);
    }

    [Fact]
    public async Task Save_MissingName_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync(Url, CreateRequest(name: ""));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Save_UnknownItemType_ReturnsBadRequest()
    {
        var json = """{ "name": "Hero", "items": [ { "type": "potion", "name": "Heal" } ] }""";
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(Url, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Save_TwoEquippedWeapons_ReturnsBadRequest()
    {
        var request = CreateRequest(items: new List<ItemDto>
        {
            CreateSword(isEquipped: true),
            new WeaponDto { Name = "Axe", IsEquipped = true }
        });

        var response = await _client.PostAsJsonAsync(Url, request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task Get_SavedCharacter_ReturnsAllCharacterFields()
    {
        var id = await SaveAsync(CreateRequest("Hero"));

        var character = await GetAsync(id);

        Assert.Equal(id, character.Id);
        Assert.Equal("Hero", character.Name);
        Assert.Equal(100, character.MaxHealth);
        Assert.Equal(80, character.Health);
        Assert.Equal(2, character.Level);
        Assert.Equal(50, character.Xp);
        Assert.Equal(10, character.BaseAttackDamage);
        Assert.Equal(1, character.BaseMagicDamage);
        Assert.Equal(5, character.BaseAttackResist);
        Assert.Equal(2, character.BaseMagicResist);
    }

    [Fact]
    public async Task Get_CharacterWithoutItems_ReturnsEmptyInventoryAndNothingEquipped()
    {
        var id = await SaveAsync(CreateRequest());

        var character = await GetAsync(id);

        Assert.Empty(character.Items);
        Assert.Null(character.EquippedWeapon);
        Assert.Null(character.EquippedProtection);
    }

    [Fact]
    public async Task Get_CharacterWithItems_ReturnsItemsWithCorrectTypeAndValues()
    {
        var id = await SaveAsync(CreateRequest(items: new List<ItemDto>
        {
            CreateSword(),
            CreatePlate()
        }));

        var character = await GetAsync(id);

        Assert.Equal(2, character.Items.Count);

        var weapon = Assert.Single(character.Items.OfType<WeaponDto>());
        Assert.Equal("Sword", weapon.Name);
        Assert.Equal("A sharp blade", weapon.Description);
        Assert.Equal(5, weapon.AttackBonus);
        Assert.Equal(3, weapon.MagicBonus);

        var protection = Assert.Single(character.Items.OfType<ProtectionDto>());
        Assert.Equal("Plate", protection.Name);
        Assert.Equal("A sturdy plate", protection.Description);
        Assert.Equal(4, protection.AttackResist);
        Assert.Equal(2, protection.MagicResist);
    }

    [Fact]
    public async Task Get_CharacterWithEquippedItems_ReturnsEquippedWeaponAndProtection()
    {
        var id = await SaveAsync(CreateRequest(items: new List<ItemDto>
        {
            CreateSword(isEquipped: true),
            CreatePlate(isEquipped: true)
        }));

        var character = await GetAsync(id);

        Assert.Equal("Sword", character.EquippedWeapon?.Name);
        Assert.Equal("Plate", character.EquippedProtection?.Name);
    }

    [Fact]
    public async Task Get_CharacterWithUnequippedItems_ReturnsNothingEquipped()
    {
        var id = await SaveAsync(CreateRequest(items: new List<ItemDto>
        {
            CreateSword(isEquipped: false),
            CreatePlate(isEquipped: false)
        }));

        var character = await GetAsync(id);

        Assert.Null(character.EquippedWeapon);
        Assert.Null(character.EquippedProtection);
        Assert.Equal(2, character.Items.Count);
    }

    [Fact]
    public async Task Get_TwoSavedCharacters_ReturnsTheRequestedOne()
    {
        var firstId = await SaveAsync(CreateRequest("First", new List<ItemDto> { CreateSword() }));
        var secondId = await SaveAsync(CreateRequest("Second"));

        var first = await GetAsync(firstId);
        var second = await GetAsync(secondId);

        Assert.Equal("First", first.Name);
        Assert.Single(first.Items);
        Assert.Equal("Second", second.Name);
        Assert.Empty(second.Items);
    }

    [Fact]
    public async Task Get_UnknownId_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"{Url}/0");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}