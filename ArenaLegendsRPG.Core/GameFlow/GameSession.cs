using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.GameFlow;

public class GameSession
{
    public Character? Player { get; private set; }

    public void SetPlayer(Character player)
    {
        Player = player;
    }

    public Character RequirePlayer()
    {
        return Player ?? throw new InvalidOperationException("No character selected or created.");
    }

}