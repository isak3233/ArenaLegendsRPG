using ArenaLegendsRPG.Core.GameFlow;

namespace ArenaLegendsRPG.UI.Rendering;

public class GameEventRender : IGameEventRender
{
    private readonly IEnumerable<IEventRender> _presenters;
    private readonly IConsoleIO _consoleIO;

    public GameEventRender(IEnumerable<IEventRender> presenters, IConsoleIO consoleIO)
    {
        _presenters = presenters;
        _consoleIO = consoleIO;
    }

    public void Present(GameEvent gameEvent)
    {
        var presenter = _presenters.FirstOrDefault(p => p.CanPresent(gameEvent));

        if (presenter is null)
        {
            _consoleIO.WriteLine($"Event saknar presenter: {gameEvent.GetType().Name}");
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