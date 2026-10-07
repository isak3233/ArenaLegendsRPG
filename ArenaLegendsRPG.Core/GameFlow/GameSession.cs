using ArenaLegendsRPG.Core.Characters;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;

namespace ArenaLegendsRPG.Core.GameFlow;

public class GameSession : IGameSession
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