using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;

namespace ArenaLegendsRPG.UI.Rendering.EventRenders;

public class InventoryEventRender : IEventRender
{
    private readonly IConsoleIO _consoleIO;
    public InventoryEventRender(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }

    public bool CanPresent(GameEvent gameEvent)
    {
        return gameEvent is WeaponEvent
            or ProtectionEvent
            or InvalidItemSelection
            or ItemEquipped
            or InventoryNoItems;
    }

    public void Present(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case WeaponEvent e:
                var wParts = new List<string>();
                if (e.Attack > 0) wParts.Add($"Fysisk skada: {e.Attack}");
                if (e.Magic > 0) wParts.Add($"Magisk skada: {e.Magic}");
                if (e.IsEquipped) wParts.Add($"(I Användning)");
                _consoleIO.WriteLine($"{e.Index}: {e.Name}: {string.Join(", ", wParts)}");
                break;
            case ProtectionEvent e:
                var pParts = new List<string>();
                if (e.Attack > 0) pParts.Add($"Fysisk-resistans: {e.Attack}");
                if (e.Magic > 0) pParts.Add($"Magi-resistans : {e.Magic}");
                if (e.IsEquipped) pParts.Add("(I Användning)");
                _consoleIO.WriteLine($"{e.Index}: {e.Name}: {string.Join(", ", pParts)}");
                break;
            case InvalidItemSelection e:
                _consoleIO.WriteLine("Du måste ange en siffran som står bredvid föremålets namn");
                _consoleIO.ReadKey();
                break;
            case ItemEquipped e:
                if (e.Equipped)
                {
                    _consoleIO.WriteLine($"Du har nu börjat använda {e.ItemName}");
                    _consoleIO.ReadKey();
                    break;
                }
                else
                {
                    _consoleIO.WriteLine($"Du har nu slutat använda {e.ItemName}");
                    _consoleIO.ReadKey();
                    break;
                }
            case InventoryNoItems e:
                _consoleIO.WriteLine("Du har inga föremål!");
                _consoleIO.ReadKey();
                break;

        }
    }
}