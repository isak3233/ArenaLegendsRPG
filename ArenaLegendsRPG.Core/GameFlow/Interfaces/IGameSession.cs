using ArenaLegendsRPG.Core.Characters;

namespace ArenaLegendsRPG.Core.GameFlow.Interfaces;

public interface IGameSession
{
    Character? Player { get; }
    void SetPlayer(Character player);
    Character RequirePlayer();
}