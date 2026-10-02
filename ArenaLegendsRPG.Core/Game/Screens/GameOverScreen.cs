using ArenaLegendsRPG.Core.Game.Interfaces;
using ArenaLegendsRPG.Core.Game.Menus;

namespace ArenaLegendsRPG.Core.Game.Screens;

public class GameOverScreen : GameScreenBase
{
    public override GameState State => GameState.GameOver;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new List<MenuAction>();
    }


    protected override ScreenResult Handle(MenuAction action)
    {
        throw new InvalidOperationException(); 
    }
        
}