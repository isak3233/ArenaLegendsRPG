using System;
using System.Collections.Generic;
using System.Text;
using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Tests
{
    public class InventoryTests
    {
        [Fact]
        public void AddItem_AddsItemToItems()
        {
            var inventory = new Inventory();
            var sword = new Item("Sword", "A sharp blade");

            inventory.AddItem(sword);
            Assert.Contains(sword, inventory.Items);
        }
        [Fact]
        public void RemoveItem_RemovesItemFromItems()
        {
            var inventory = new Inventory();
            var sword = new Item("Sword", "A sharp blade");

            inventory.RemoveItem(sword);
            Assert.DoesNotContain(sword, inventory.Items);
        }
    }
}
