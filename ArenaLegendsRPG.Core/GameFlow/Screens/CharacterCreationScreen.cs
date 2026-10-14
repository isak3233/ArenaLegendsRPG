
using ArenaLegendsRPG.Core.APIHandlers;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;
using ArenaLegendsRPG.Core.GameServices.Results;


namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class CharacterCreationScreen : IGameScreen, ITextInputScreen, ISelectionScreen
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    private readonly ICharacterCreationService _characterCreationService;
    private readonly List<LoadedCharacterResult> _loadedCharacters = new();

    public CharacterCreationScreen(IGameSession session, IScreenFactory factory, ICharacterCreationService characterCreationService)
    {
        _session = session;
        _factory = factory;
        _characterCreationService = characterCreationService;
    }

    public GameState State => GameState.CharacterCreation;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.CreateNewCharacter, MenuAction.LoadCharacter };
    }

    public async Task<ScreenResult> ChooseAsync(MenuAction action)
    {
        return action switch
        {
            MenuAction.CreateNewCharacter => ScreenResult.To(this, new CreateNewCharacter()),
            MenuAction.LoadCharacter => await HandleLoadingCharacter(),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }

    public async Task<ScreenResult> HandleLoadingCharacter()
    {
        var characterResults = await _characterCreationService.LoadAllCharacters();

        if (characterResults.Count == 0)
        {
            return ScreenResult.To(this, new NoSavedCharacters());
        }
        _loadedCharacters.Clear();
        _loadedCharacters.AddRange(characterResults);

        var events = characterResults
            .Select((r, index) => (GameEvent)new LoadedCharacter(index + 1, r.Character))
            .Append(new LoadSuccess())
            .ToArray();

        return ScreenResult.To(this, events);
    }

    public ScreenResult Select(int index)
    {
        index--;
        if (index < 0 || index >= _loadedCharacters.Count)
        {
            return ScreenResult.To(this, new InvalidCharacterSelection());
        }
        var character = _loadedCharacters[index].Character;
        _session.SetPlayer(character);
        return ScreenResult.To(_factory.CreateGameMenu(), new CharacterLoaded());
    }
    public ScreenResult Submit(string text)
    {
        var nameIsAllowed = _characterCreationService.ValidateCharacterName(text);
        if (nameIsAllowed == false)
        {
            return ScreenResult.To(this, new CharacterNameNotAllowed());
        }
        var character = _characterCreationService.CreateCharacter(text);

        _session.SetPlayer(character);

        return ScreenResult.To(_factory.CreateGameMenu(), new CharacterCreated(character.Name));
    }
}