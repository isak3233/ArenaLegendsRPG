using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class CharacterStatsScreen : IGameScreen
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    public GameState State => GameState.CharacterStats;

    public CharacterStatsScreen(IGameSession session, IScreenFactory factory)
    {
        _session = session;
        _factory = factory;
    }
    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.ShowStats, MenuAction.Back };
    }

    public async Task<ScreenResult> ChooseAsync(MenuAction action)
    {
        return action switch
        {
            MenuAction.ShowStats => HandleShowStats(),
            MenuAction.Back => ScreenResult.To(_factory.CreateGameMenu())
        };
    }

    public ScreenResult HandleShowStats()
    {
        var characterStats = new CharacterStats(_session.RequirePlayer());
        return ScreenResult.To(this, characterStats);
    }
}