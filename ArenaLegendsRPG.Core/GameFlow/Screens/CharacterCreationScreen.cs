using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class CharacterCreationScreen : GameScreenBase, ITextInputScreen
{
    private readonly GameSession _session;
    private readonly IScreenFactory _factory;
    private readonly ICharacterCreationService _characterCreationService;

    public CharacterCreationScreen(GameSession session, IScreenFactory factory, ICharacterCreationService characterCreationService)
    {
        _session = session;
        _factory = factory;
        _characterCreationService = characterCreationService;
    }

    public override GameState State => GameState.CharacterCreation;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return Array.Empty<MenuAction>();
    }

    protected override ScreenResult Handle(MenuAction action)
    {
        throw new NotImplementedException();
    }

    public ScreenResult Submit(string text)
    {
        var character = _characterCreationService.CreateCharacter(text);
        _session.SetPlayer(character);

        return ScreenResult.To(_factory.CreateGameMenu(), new CharacterCreated(character.Name));
    }
}