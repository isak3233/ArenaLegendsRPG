using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameFlow.Screens;
using ArenaLegendsRPG.Core.GameServices.Results;
using ArenaLegendsRPG.Tests.TestDoubles;

namespace ArenaLegendsRPG.Tests.CoreTests.ScreenTests;

public class CharacterCreationScreenTests
{
    private readonly GameSession _session = new();
    private readonly StubGameScreen _gameMenu = new(GameState.GameMenu);
    private readonly StubCharacterCreationService _service = new();
    private readonly CharacterCreationScreen _sut;

    public CharacterCreationScreenTests()
    {
        var factory = new StubScreenFactory(gameMenu: _gameMenu);
        _sut = new CharacterCreationScreen(_session, factory, _service);
    }

    private static LoadedCharacterResult CreateSave(string name, int savedId) =>
        new(new Character(name, 100, 1, 0, 10, 0, 5, 2));

    private async Task LoadSavesAsync(params LoadedCharacterResult[] saves)
    {
        _service.Loaded = saves;
        await _sut.ChooseAsync(MenuAction.LoadCharacter);
    }


    [Fact]
    public void GetAvailableActions_ReturnsCreateNewAndLoad()
    {
        Assert.Equal(new[] { MenuAction.CreateNewCharacter, MenuAction.LoadCharacter }, _sut.GetAvailableActions());
    }

    [Fact]
    public async Task Choose_CreateNewCharacter_StaysOnSameScreen()
    {
        var result = await _sut.ChooseAsync(MenuAction.CreateNewCharacter);

        Assert.Same(_sut, result.Next);
    }

    [Fact]
    public async Task Choose_CreateNewCharacter_ReturnsCreateNewCharacterEvent()
    {
        var result = await _sut.ChooseAsync(MenuAction.CreateNewCharacter);

        Assert.Contains(result.Events, e => e is CreateNewCharacter);
    }

    [Fact]
    public async Task Choose_ActionNotInMenu_Throws()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.ChooseAsync(MenuAction.Attack));
    }


    [Fact]
    public async Task Choose_LoadCharacter_StaysOnSameScreen()
    {
        _service.Loaded = new[] { CreateSave("Hero", 3) };

        var result = await _sut.ChooseAsync(MenuAction.LoadCharacter);

        Assert.Same(_sut, result.Next);
    }

    [Fact]
    public async Task Choose_LoadCharacter_ReturnsLoadSuccessEvent()
    {
        _service.Loaded = new[] { CreateSave("Hero", 3) };

        var result = await _sut.ChooseAsync(MenuAction.LoadCharacter);

        Assert.Contains(result.Events, e => e is LoadSuccess);
    }

    [Fact]
    public async Task Choose_LoadCharacter_ReturnsLoadedCharacterEventPerSave_InOrder()
    {
        _service.Loaded = new[] { CreateSave("Hero", 3), CreateSave("Mage", 8) };

        var result = await _sut.ChooseAsync(MenuAction.LoadCharacter);

        var loaded = result.Events.OfType<LoadedCharacter>().ToList();
        Assert.Equal(new[] { "Hero", "Mage" }, loaded.Select(e => e.Character.Name));
    }

    [Fact]
    public async Task Choose_LoadCharacter_NoSaves_StaysOnSameScreen()
    {
        _service.Loaded = Array.Empty<LoadedCharacterResult>();

        var result = await _sut.ChooseAsync(MenuAction.LoadCharacter);

        Assert.Same(_sut, result.Next);
    }

    [Fact]
    public async Task Choose_LoadCharacter_NoSaves_ReturnsNoSavedCharactersEvent()
    {
        _service.Loaded = Array.Empty<LoadedCharacterResult>();

        var result = await _sut.ChooseAsync(MenuAction.LoadCharacter);

        Assert.Contains(result.Events, e => e is NoSavedCharacters);
        Assert.DoesNotContain(result.Events, e => e is LoadSuccess);
        Assert.DoesNotContain(result.Events, e => e is LoadedCharacter);
    }


    [Fact]
    public async Task Select_ValidIndex_GoesToGameMenu()
    {
        await LoadSavesAsync(CreateSave("Hero", 3));

        var result = _sut.Select(1);

        Assert.Same(_gameMenu, result.Next);
    }

    [Fact]
    public async Task Select_ValidIndex_ReturnsCharacterLoadedEvent()
    {
        await LoadSavesAsync(CreateSave("Hero", 3));

        var result = _sut.Select(1);

        Assert.Contains(result.Events, e => e is CharacterLoaded);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(99)]
    public async Task Select_IndexOutOfRange_StaysOnSameScreen(int index)
    {
        await LoadSavesAsync(CreateSave("Hero", 3), CreateSave("Mage", 8));

        var result = _sut.Select(index);

        Assert.Same(_sut, result.Next);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    [InlineData(99)]
    public async Task Select_IndexOutOfRange_ReturnsInvalidCharacterSelectionEvent(int index)
    {
        await LoadSavesAsync(CreateSave("Hero", 3), CreateSave("Mage", 8));

        var result = _sut.Select(index);

        Assert.Contains(result.Events, e => e is InvalidCharacterSelection);
        Assert.DoesNotContain(result.Events, e => e is CharacterLoaded);
    }

    [Fact]
    public void Select_BeforeAnythingIsLoaded_ReturnsInvalidCharacterSelectionEvent()
    {
        var result = _sut.Select(0);

        Assert.Contains(result.Events, e => e is InvalidCharacterSelection);
    }

    [Fact]
    public async Task Select_AfterLoadingAgain_OnlyAcceptsIndexesFromTheLatestList()
    {
        await LoadSavesAsync(CreateSave("Old", 1), CreateSave("Older", 2));
        await LoadSavesAsync(CreateSave("New", 3));

        var result = _sut.Select(2);

        Assert.Contains(result.Events, e => e is InvalidCharacterSelection);
    }

    [Fact]
    public void Submit_ReturnsCharacterCreatedEvent()
    {
        var result = _sut.Submit("Hero");

        Assert.Contains(result.Events, e => e is CharacterCreated created && created.PlayerName == "Hero");
    }

    [Fact]
    public void Submit_ReturnsGameMenuAsNextScreen()
    {
        var result = _sut.Submit("Hero");

        Assert.Same(_gameMenu, result.Next);
    }

    [Fact]
    public void Submit_InvalidName_StaysOnSameScreen()
    {
        _service.NameIsValid = false;

        var result = _sut.Submit("Hero");

        Assert.Same(_sut, result.Next);
    }

    [Fact]
    public void Submit_InvalidName_ReturnsCharacterNameNotAllowedEvent()
    {
        _service.NameIsValid = false;

        var result = _sut.Submit("Hero");

        Assert.Contains(result.Events, e => e is CharacterNameNotAllowed);
        Assert.DoesNotContain(result.Events, e => e is CharacterCreated);
    }
}