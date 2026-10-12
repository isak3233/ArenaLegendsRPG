using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.Input;

public class GameEventInputRouter
{
    private readonly InputHelper _inputHelper;

    private readonly Dictionary<Type, Func<GameEvent, InputResult>> _handlers;

    public GameEventInputRouter(InputHelper inputHelper)
    {
        _inputHelper = inputHelper;

        _handlers = new Dictionary<Type, Func<GameEvent, InputResult>>
        {
            [typeof(CreateNewCharacter)] =
                _ => new InputResult(
                    InputType.String,
                    _inputHelper.GetString("Skriv karaktärens namn: ")
                )
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