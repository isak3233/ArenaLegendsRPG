namespace ArenaLegendsRPG.Core.GameFlow.GameEvents
{
    public record ItemFound(string PlayerName, string ItemName) : GameEvent;

}
