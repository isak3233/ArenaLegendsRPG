using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class BattleLogScreen : IGameScreen
{
    private readonly IScreenFactory _factory;
    private readonly IGameSession _gameSession;
    private readonly IBattleLogService _battleLogService;
    public BattleLogScreen(IGameSession gameSession, IScreenFactory factory, IBattleLogService battleLogService)
    {
        _factory = factory;
        _gameSession = gameSession;
        _battleLogService = battleLogService;
    }
    public GameState State => GameState.BattleLog;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.ShowBattleLogs, MenuAction.Back };
    }

    public async Task<ScreenResult> ChooseAsync(MenuAction action)
    {
        return action switch
        {
            MenuAction.ShowBattleLogs => await HandleShowBattleLogsAsync(),
            MenuAction.Back => ScreenResult.To(_factory.CreateGameMenu()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }

    private async Task<ScreenResult> HandleShowBattleLogsAsync()
    {
        var battleLogs = await _battleLogService.GetBattleLogs(_gameSession.RequirePlayer().SavedPlayerId);
        var battleLogEvents = battleLogs
            .Select(battleLog => (GameEvent)new BattleLogInfo(
                battleLog.MonsterName,
                battleLog.MonsterHealth,
                battleLog.MonsterAttackResist,
                battleLog.MonsterMagicResist,
                battleLog.MonsterAttackDamage,
                battleLog.MonsterMagicDamage,
                battleLog.DamageDoneByPlayer,
                battleLog.DamageDoneByMonster,
                battleLog.IsPlayerWinner,
                battleLog.BattleFinished))
            .Append(new BattleLogResult())
            .ToArray();
        return ScreenResult.To(this, battleLogEvents);
    }
}