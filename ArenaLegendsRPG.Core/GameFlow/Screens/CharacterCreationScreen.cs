using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
<<<<<<< HEAD
using ArenaLegendsRPG.Core.GameServices;
=======
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
>>>>>>> c7c5ff90c9f6b06a7e1c0813129d3e05b5300166

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
<<<<<<< HEAD
        var character = _characterCreationService.CreateCharacter(text);
=======
        //Check if name should be able to be taken. 
        var character = new Character(
            name: text,
            health: 100,
            baseAttackDamage: 10,
            baseMagicDamage: 5,
            baseAttackResist: 5,
            baseMagicResist: 2
            );


>>>>>>> c7c5ff90c9f6b06a7e1c0813129d3e05b5300166
        _session.SetPlayer(character);

        return ScreenResult.To(_factory.CreateGameMenu(), new CharacterCreated(character.Name));
    }
}