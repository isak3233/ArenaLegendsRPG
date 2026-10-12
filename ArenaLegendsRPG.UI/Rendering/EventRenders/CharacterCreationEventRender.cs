using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

public class CharacterCreationEventRender : IEventRender
{
    private readonly IConsoleIO _consoleIO;
    public CharacterCreationEventRender(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is CharacterNameNotAllowed or CharacterCreated;
    }
    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case CharacterNameNotAllowed e:
                _consoleIO.WriteLine("Namnet måste vara giltigt. Får inte vara tomt eller ha mer än 20 tecken.");
                _consoleIO.ReadKey();
                break;
            case CharacterCreated e:
                _consoleIO.WriteLine("Karaktären skapades!");
                _consoleIO.ReadKey();
                break;
        }
    }
}