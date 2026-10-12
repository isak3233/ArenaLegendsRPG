using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.UI.Rendering;

public class GameEventRender
{
    private readonly IEnumerable<IGameEventRender> _presenters;

    public GameEventRender(
        IEnumerable<IGameEventRender> presenters)
    {
        _presenters = presenters;
    }

    public void Present(GameEvent gameEvent)
    {
        var presenter = _presenters
            .FirstOrDefault(p => p.CanPresent(gameEvent));

        if (presenter is null)
        {
            Console.WriteLine(
                $"Event saknar presenter: {gameEvent.GetType().Name}"
            );
            return;
        }

        presenter.Present(gameEvent);
    }

    public void PresentAll(IEnumerable<GameEvent> events)
    {
        foreach (var gameEvent in events)
        {
            Present(gameEvent);
        }
    }
}