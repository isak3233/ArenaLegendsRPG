using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.Input;

public class GameEventInputRouter : IGameEventInputRouter
{
    private readonly IInputHelper _inputHelper;

    private readonly Dictionary<Type, Func<GameEvent, InputResult>> _handlers;

    public GameEventInputRouter(IInputHelper inputHelper)
    {
        _inputHelper = inputHelper;

        _handlers = new Dictionary<Type, Func<GameEvent, InputResult>>
        {
            [typeof(CreateNewCharacter)] =
                _ => new InputResult(
                    InputType.String,
                    _inputHelper.GetString("Skriv karaktärens namn: ")
                ),
            [typeof(LoadSuccess)] =
                    _ => new InputResult(
                        InputType.Integer,
                        _inputHelper.GetInt("Välj mellan de sparade karaktärerna: ")),
            [typeof(InventoryManagmentOpen)] =
                _ => new InputResult(
                    InputType.Integer,
                    _inputHelper.GetInt("Välj item för att börja/sluta använda det: "))
        };
    }

    public InputResult? Handle(GameEvent gameEvent)
    {
        if (_handlers.TryGetValue(gameEvent.GetType(), out var handler))
        {
            return handler(gameEvent);
        }

        return null;
    }
}