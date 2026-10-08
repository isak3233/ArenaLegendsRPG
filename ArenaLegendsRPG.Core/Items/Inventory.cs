
using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.Items
{
    public class Inventory : IInventory
    {
        private readonly List<IItem> _items = new List<IItem>();

        public IReadOnlyList<IItem> GetItems()
        {
            return _items;
        }
        public void AddItem(IItem item)
        {
            _items.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            _items.Remove(item);
        }
    }

}

