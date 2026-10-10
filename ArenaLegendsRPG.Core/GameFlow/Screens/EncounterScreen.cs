using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;


namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class EncounterScreen : GameScreenBase
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    private readonly ICombatService _combatService;
    private readonly ILevelingService _levelingService;
    public IEncounter CurrentEncounter { get; }

    

    public EncounterScreen(IGameSession session, IScreenFactory factory, IEncounterService encounterService, ICombatService combatService, ILevelingService levelingService)
    {
        _session = session;
        _factory = factory;
        _combatService = combatService;
        _levelingService = levelingService;
        CurrentEncounter = encounterService.GenerateEncounter();
    }

    public override GameState State => GameState.InEncounter;

    public override IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return CurrentEncounter switch
        {
            MonsterEncounter => new[] { MenuAction.Attack, MenuAction.Flee },
            ChestEncounter => new[] { MenuAction.OpenChest, MenuAction.LeaveEncounter },
            StructureEncounter => new[] { MenuAction.LeaveEncounter },
            _ => throw new InvalidOperationException($"Unhandled encounter type{CurrentEncounter.GetType().Name}")
        };
    }

    protected override ScreenResult Handle(MenuAction action)
    {
        return action switch
        {
            MenuAction.Attack => HandleAttack(),
            MenuAction.Flee => HandleFlee(),
            MenuAction.OpenChest => throw new NotImplementedException("Chest opening not built"),
            MenuAction.LeaveEncounter => ScreenResult.To(_factory.CreateGameMenu()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }

    private ScreenResult HandleAttack()
    {
        var monsterEncounter = (MonsterEncounter)CurrentEncounter;
        var player = _session.RequirePlayer();

        var result = _combatService.ProcessPlayerAttack(player, monsterEncounter.Monster);
        if (result.MonsterDied)
        {
            var xpEvents = _levelingService.GainXp(player, monsterEncounter.Monster.XpReward);
            var events = result.Events.Concat(xpEvents).ToArray();
            return ScreenResult.To(_factory.CreateGameMenu(), events);
        }

        if (result.PlayerDied)
        {
            return ScreenResult.To(_factory.CreateGameOver(), result.Events.ToArray());
        }

        return ScreenResult.To(this, result.Events.ToArray());
    }

    private ScreenResult HandleFlee()
    {
        var result = _combatService.AttemptFlee();

        if (result.Succeeded)
        {
            return ScreenResult.To(_factory.CreateGameMenu(), result.Events.ToArray());
        }

        return ScreenResult.To(this, result.Events.ToArray());
    }
}
