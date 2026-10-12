using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

public class CharacterCreationEventRender : IGameEventRender
{
    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is CharacterNameNotAllowed or CharacterCreated;
    }
    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case CharacterNameNotAllowed e:
                Console.WriteLine("Namnet måste vara giltigt. Får inte vara tomt eller ha mer än 20 tecken.");
                Console.ReadKey();
                break;
            case CharacterCreated e:
                Console.WriteLine("Karaktären skapades!");
                Console.ReadKey();
                break;
        }
    }
}