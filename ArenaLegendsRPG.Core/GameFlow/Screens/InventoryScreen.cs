using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class InventoryScreen : IGameScreen, ISelectionScreen
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    private readonly IInventoryService _inventoryService;
    private IReadOnlyList<GameEvent> _itemEvents;

    public InventoryScreen(IGameSession session, IScreenFactory factory, IInventoryService inventoryService)
    {
        _session = session;
        _factory = factory;
        _inventoryService = inventoryService;
    }

    public GameState State => GameState.Inventory;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        _itemEvents = _inventoryService.GetItemEvents(_session.RequirePlayer());
        return new[] { MenuAction.OpenInventoryManagment, MenuAction.Back };
    }

    public async Task<ScreenResult> ChooseAsync(MenuAction action)
    {
        return action switch
        {
            MenuAction.OpenInventoryManagment => HandleOpenInventoryManagment(),
            MenuAction.Back => ScreenResult.To(_factory.CreateGameMenu()),
            _ => throw new InvalidOperationException($"{action} is not available in {State}.")
        };
    }

    private ScreenResult HandleOpenInventoryManagment()
    {
        var itemEvents = _inventoryService.GetItemEvents(_session.RequirePlayer());
        if (itemEvents.Count == 0)
        {
            return ScreenResult.To(this, new InventoryNoItems());
        }
        itemEvents.Add(new InventoryManagmentOpen());
        return ScreenResult.To(this, itemEvents.ToArray());
    }

    public ScreenResult Select(int index)
    {
        index--;
        var events = _inventoryService.ToggleEquiped(_session.RequirePlayer(), index);

        return ScreenResult.To(this, events.ToArray());
    }
}