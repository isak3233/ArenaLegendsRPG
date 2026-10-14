using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

public class GameMenuEventRender : IEventRender
{
    private readonly IConsoleIO _consoleIO;
    public GameMenuEventRender(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is SaveSuccess;
    }

    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case SaveSuccess e:
                _consoleIO.WriteLine("Lyckades spara!");
                _consoleIO.ReadKey();
                break;
            case SaveFailed e:
                _consoleIO.WriteLine("Lyckades inte spara :(");
                _consoleIO.ReadKey();
                break;
        }
    }
}