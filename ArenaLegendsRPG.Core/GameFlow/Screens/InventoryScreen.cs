using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.Core.GameServices.GameServiceInterfaces;

namespace ArenaLegendsRPG.Core.GameFlow.Screens;

public class InventoryScreen : IGameScreen, ISelectionScreen
{
    private readonly IGameSession _session;
    private readonly IScreenFactory _factory;
    private readonly IInventoryService _inventoryService;

    public InventoryScreen(IGameSession session, IScreenFactory factory, IInventoryService inventoryService)
    {
        _session = session;
        _factory = factory;
        _inventoryService = inventoryService;
    }

    public GameState State => GameState.Inventory;

    public IReadOnlyList<MenuAction> GetAvailableActions()
    {
        return new[] { MenuAction.Back };
    }

    public ScreenResult Choose(MenuAction action)
    {
        return action switch
        {
            MenuAction.Back => ScreenResult.To(_factory.CreateGameMenu()),
            _ => throw new InvalidOperationException($"{action} is not available in {State}.")
        };
    }

    public ScreenResult Select(int index)
    {
        var events = _inventoryService.EquipItem(_session.RequirePlayer(), index);

        return ScreenResult.To(this, events.ToArray());
    }
}