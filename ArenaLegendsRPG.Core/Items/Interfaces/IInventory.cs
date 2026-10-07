namespace ArenaLegendsRPG.Core.Items.Interfaces;

public interface IInventory
{
    public IReadOnlyList<IItem> GetItems();
    public void AddItem(IItem item);
    public void RemoveItem(IItem item);
}