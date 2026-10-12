using ArenaLegendsRPG.Core.Encounters;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;

using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;


namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class EncounterScreen : IGameScreen
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    private readonly ICombatService _combatService;
    private readonly ILevelingService _levelingService;
    private readonly IEncounterService _encounterService;
    public IEncounter? CurrentEncounter { get; set; }
    private bool _firstEncounter = true;



    public EncounterScreen(IGameSession session, IScreenFactory factory, IEncounterService encounterService, ICombatService combatService, ILevelingService levelingService)
    {
        _session = session;
        _factory = factory;
        _combatService = combatService;
        _levelingService = levelingService;
        _encounterService = encounterService;

    }

    public GameState State => GameState.InEncounter;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        if (CurrentEncounter == null)
        {
            if (_firstEncounter)
            {
                _firstEncounter = false;
                return new[] { MenuAction.BeginExplore, MenuAction.LeaveEncounterScreen };
            }
            else
            {
                return new[] { MenuAction.ContinueExploring, MenuAction.LeaveEncounterScreen };
            }

        }

        return CurrentEncounter switch
        {
            MonsterEncounter => new[] { MenuAction.Attack, MenuAction.Flee },
            ChestEncounter => new[] { MenuAction.OpenChest, MenuAction.LeaveEncounter },
            StructureEncounter => new[] { MenuAction.LeaveEncounter },
            _ => throw new InvalidOperationException($"Unhandled encounter type{CurrentEncounter.GetType().Name}")
        };
    }

    public ScreenResult Choose(MenuAction action)
    {
        return action switch
        {
            MenuAction.BeginExplore => HandleExplore(),
            MenuAction.ContinueExploring => HandleExplore(),
            MenuAction.Attack => HandleAttack(),
            MenuAction.Flee => HandleFlee(),
            MenuAction.OpenChest => throw new NotImplementedException("Chest opening not built"),
            MenuAction.LeaveEncounter => HandleLeaveEncounter(),
            MenuAction.LeaveEncounterScreen => ScreenResult.To(_factory.CreateGameMenu()),
            _ => throw new InvalidOperationException($"Unhandled action {action} in {State}.")
        };
    }

    private ScreenResult HandleLeaveEncounter()
    {
        CurrentEncounter = null;
        return ScreenResult.To(this);
    }
    private ScreenResult HandleExplore()
    {
        CurrentEncounter = _encounterService.GenerateEncounter();
        return CurrentEncounter switch
        {
            MonsterEncounter => ScreenResult.To(this, new MonsterEncounterStarted("MONSTER NAME NOT IMPLEMENTED")),
            ChestEncounter => ScreenResult.To(this, new ChestEncounterStarted()),
            StructureEncounter => ScreenResult.To(this, new StructureEncounterStarted()),
            _ => throw new InvalidOperationException($"Unhandled encounter type {CurrentEncounter.GetType().Name}")
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
            CurrentEncounter = null;
            return ScreenResult.To(this, events);
        }

        if (result.PlayerDied)
        {
            return ScreenResult.To(_factory.CreateGameOver(), result.Events.ToArray());
        }
        CurrentEncounter = null;
        return ScreenResult.To(this, result.Events.ToArray());
    }

    private ScreenResult HandleFlee()
    {
        var result = _combatService.AttemptFlee();

        if (result.Succeeded)
        {
            CurrentEncounter = null;
        }

        return ScreenResult.To(this, result.Events.ToArray());
    }
}
