
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;


namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class CharacterCreationScreen : IGameScreen, ITextInputScreen
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    private readonly ICharacterCreationService _characterCreationService;

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

    public ScreenResult Choose(MenuAction action)
    {
        return action switch
        {
            MenuAction.CreateNewCharacter => ScreenResult.To(this, new CreateNewCharacter()),
            MenuAction.LoadCharacter => throw new NotImplementedException(),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
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