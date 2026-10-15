using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.Items;

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
                var weaponParts = new List<string>();

                if (e.Attack > 0)
                {
                    weaponParts.Add($"Fysisk skada: {e.Attack}");
                }

                if (e.Magic > 0)
                {
                    weaponParts.Add($"Magisk skada: {e.Magic}");
                }

                if (e.IsEquipped)
                {
                    weaponParts.Add("(I användning)");
                }

                _consoleIO.WriteLine(
                    $"{e.Index}: [{GetRarityText(e.Rarity)}] {e.Name}: " +
                    string.Join(", ", weaponParts));
                break;

            case ProtectionEvent e:
                var protectionParts = new List<string>();

                if (e.Attack > 0)
                {
                    protectionParts.Add(
                        $"Fysisk resistans: {e.Attack}");
                }

                if (e.Magic > 0)
                {
                    protectionParts.Add(
                        $"Magisk resistans: {e.Magic}");
                }

                if (e.IsEquipped)
                {
                    protectionParts.Add("(I användning)");
                }

                _consoleIO.WriteLine(
                    $"{e.Index}: [{GetRarityText(e.Rarity)}] {e.Name}: " +
                    string.Join(", ", protectionParts));
                break;

            case InvalidItemSelection:
                _consoleIO.WriteLine(
                    "Du måste ange siffran som står bredvid föremålets namn.");
                _consoleIO.ReadKey();
                break;

            case ItemEquipped e:
                if (e.Equipped)
                {
                    _consoleIO.WriteLine(
                        $"Du har nu börjat använda {e.ItemName}.");
                }
                else
                {
                    _consoleIO.WriteLine(
                        $"Du har nu slutat använda {e.ItemName}.");
                }

                _consoleIO.ReadKey();
                break;

            case InventoryNoItems:
                _consoleIO.WriteLine("Du har inga föremål!");
                _consoleIO.ReadKey();
                break;
        }
    }

    private static string GetRarityText(ItemRarity rarity)
    {
        return rarity switch
        {
            ItemRarity.Common => "Vanlig",
            ItemRarity.Rare => "Sällsynt",
            ItemRarity.Epic => "Episk",
            ItemRarity.Legendary => "Legendarisk",
            _ => rarity.ToString()
        };
    }
}