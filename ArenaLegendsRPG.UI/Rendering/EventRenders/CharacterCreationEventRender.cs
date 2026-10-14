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
        return gameEvent is CharacterNameNotAllowed 
            or CharacterCreated
            or NoSavedCharacters
            or InvalidCharacterSelection
            or LoadedCharacter;
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
            case NoSavedCharacters e:
                _consoleIO.WriteLine("Du har inga sparade karaktärer");
                _consoleIO.ReadKey();
                break;
            case InvalidCharacterSelection e:
                _consoleIO.WriteLine("Du måste ange en siffran som står bredvid karaktärens namn");
                _consoleIO.ReadKey();
                break;
            case LoadedCharacter e:
                _consoleIO.WriteLine($"{e.SavedId}: {e.Character.Name} Level {e.Character.Level}");
                break;
        }
    }
}