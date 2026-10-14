using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ServiceTests;



public class CharacterCreationServiceTest : IDisposable
{


    private readonly string _filePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.txt");
    private readonly FakeCharacterRepository _repository = new();

    private CharacterCreationService CreateSut() => new CharacterCreationService(_repository, _filePath);

    private static Character CreateCharacter(string name) => new(name, 100, 1, 0, 10, 0, 5, 2);

    private void WriteFile(params string[] lines) => File.WriteAllLines(_filePath, lines);

    public void Dispose()
    {
        if (File.Exists(_filePath)) File.Delete(_filePath);
    }
    [Theory]
    [InlineData("Hero", true)]
    [InlineData("A", true)]
    [InlineData("12345678901234567890", true)]
    [InlineData("123456789012345678901", false)]
    [InlineData("", false)]
    [InlineData("   ", false)]
    [InlineData("  Hero  ", true)]
    public void ValidateCharacterName_ReturnsExpected(string name, bool expected)
    {
        Assert.Equal(expected, CreateSut().ValidateCharacterName(name));
    }

    [Fact]
    public void ValidateCharacterName_Null_ReturnsFalse()
    {
        Assert.False(CreateSut().ValidateCharacterName(null!));
    }

    [Fact]
    public void CreateCharacter_ReturnsCharacter()
    {
        var result = CreateSut().CreateCharacter("Hero");

        Assert.Equal("Hero", result.Name);
    }
    [Fact]
    public async Task SaveAsync_ReturnsSuccess()
    {
        var result = await CreateSut().SaveAsync(CreateCharacter("Hero"));

        Assert.True(result.Success);
    }

    [Fact]
    public async Task SaveAsync_WritesSavedIdToFile()
    {
        await CreateSut().SaveAsync(CreateCharacter("Hero"));

        Assert.Equal(new[] { "1" }, File.ReadAllLines(_filePath));
    }

    [Fact]
    public async Task SaveAsync_KeepsIdsAlreadyInTheFile()
    {
        WriteFile("7");

        await CreateSut().SaveAsync(CreateCharacter("Hero"));

        Assert.Equal(new[] { "7", "1" }, File.ReadAllLines(_filePath));
    }

    [Fact]
    public async Task SaveAsync_TwoSaves_WriteOneLineEach()
    {
        var sut = CreateSut();

        await sut.SaveAsync(CreateCharacter("First"));
        await sut.SaveAsync(CreateCharacter("Second"));

        Assert.Equal(new[] { "1", "2" }, File.ReadAllLines(_filePath));
    }

    [Fact]
    public async Task SaveAsync_RepositoryFails_ReturnsFailureWithMessage()
    {
        var failing = new FakeCharacterRepository { FailWith = new HttpRequestException("API is down") };
        var sut = new CharacterCreationService(failing, _filePath);

        var result = await sut.SaveAsync(CreateCharacter("Hero"));

        Assert.False(result.Success);
        Assert.Equal("API is down", result.ErrorMessage);
    }

    [Fact]
    public async Task SaveAsync_RepositoryFails_WritesNothingToFile()
    {
        var failing = new FakeCharacterRepository { FailWith = new HttpRequestException() };
        var sut = new CharacterCreationService(failing, _filePath);

        await sut.SaveAsync(CreateCharacter("Hero"));

        Assert.False(File.Exists(_filePath));
    }

    [Fact]
    public async Task SaveAsync_ThenLoadAllCharacters_ReturnsTheSavedCharacter()
    {
        var hero = CreateCharacter("Hero");
        var sut = CreateSut();

        await sut.SaveAsync(hero);
        var loaded = await sut.LoadAllCharacters();

        var result = Assert.Single(loaded);
        Assert.Same(hero, result.Character);
        Assert.Equal(1, result.Character.SavedPlayerId);
    }
    [Fact]
    public async Task LoadAllCharacters_FileMissing_ReturnsEmptyList()
    {
        var result = await CreateSut().LoadAllCharacters();

        Assert.Empty(result);
    }

    [Fact]
    public async Task LoadAllCharacters_EmptyFile_ReturnsEmptyList()
    {
        WriteFile();

        var result = await CreateSut().LoadAllCharacters();

        Assert.Empty(result);
    }

    [Fact]
    public async Task LoadAllCharacters_ReturnsCharacterAndSavedIdForEachLine()
    {
        var hero = CreateCharacter("Hero");
        var mage = CreateCharacter("Mage");
        _repository.Add(3, hero);
        _repository.Add(8, mage);
        WriteFile("3", "8");

        var result = await CreateSut().LoadAllCharacters();

        Assert.Equal(2, result.Count);
        Assert.Same(hero, result[0].Character);
        Assert.Equal(3, result[0].Character.SavedPlayerId);
        Assert.Same(mage, result[1].Character);
        Assert.Equal(8, result[1].Character.SavedPlayerId);
    }

    [Fact]
    public async Task LoadAllCharacters_KeepsOrderFromFile()
    {
        _repository.Add(1, CreateCharacter("First"));
        _repository.Add(2, CreateCharacter("Second"));
        WriteFile("2", "1");

        var result = await CreateSut().LoadAllCharacters();

        Assert.Equal(new[] { 2, 1 }, result.Select(r => r.Character.SavedPlayerId));
    }

    [Fact]
    public async Task LoadAllCharacters_IgnoresBlankAndNonNumericLines()
    {
        _repository.Add(5, CreateCharacter("Hero"));
        WriteFile("", "abc", "  5  ", "12x");

        var result = await CreateSut().LoadAllCharacters();

        var loaded = Assert.Single(result);
        Assert.Equal(5, loaded.Character.SavedPlayerId);
    }

    [Fact]
    public async Task LoadAllCharacters_IdNotFoundInRepository_IsSkipped()
    {
        _repository.Add(1, CreateCharacter("Hero"));
        WriteFile("1", "99");

        var result = await CreateSut().LoadAllCharacters();

        Assert.Equal(1, Assert.Single(result).Character.SavedPlayerId);
    }

    [Fact]
    public async Task LoadAllCharacters_RepositoryFailsForOneId_StillLoadsTheOthers()
    {
        _repository.Add(1, CreateCharacter("Hero"));
        _repository.Add(2, CreateCharacter("Broken"));
        _repository.Add(3, CreateCharacter("Mage"));
        _repository.FailLoadingOf(2);
        WriteFile("1", "2", "3");

        var result = await CreateSut().LoadAllCharacters();

        Assert.Equal(new[] { 1, 3 }, result.Select(r => r.Character.SavedPlayerId));
    }

}