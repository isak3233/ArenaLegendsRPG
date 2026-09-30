using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaLegendsRPG.Core.Items
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();

        public IReadOnlyList<Item> GetItems()
        {
            return _items;
        }
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }
    }

}

