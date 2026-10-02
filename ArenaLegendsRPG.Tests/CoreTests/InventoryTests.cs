using ArenaLegendsRPG.Core.Items;

namespace ArenaLegendsRPG.Tests.ItemTests
{
    public class InventoryTests
    {
        [Fact]
        public void GetItems_NewInventory_IsEmpty()
        {
            var inventory = new Inventory();

            Assert.Empty(inventory.GetItems());
        }
        [Fact]
        public void AddItem_AddsItemToItems()
        {
            var inventory = new Inventory();
            var sword = new Weapon("Sword", "A sharp blade", 10);

            inventory.AddItem(sword);

            Assert.Equal(new[] { sword }, inventory.GetItems());
        }
        [Fact]
        public void RemoveItem_RemovesOnlyThatItem()
        {
            var inventory = new Inventory();
            var sword = new Weapon("Sword", "A sharp blade", 10, 10);
            var axe = new Weapon("Axe", "A heavy axe", 15);
            inventory.AddItem(sword);
            inventory.AddItem(axe);

            inventory.RemoveItem(sword);

            Assert.Equal(new[] { axe }, inventory.GetItems());
        }

        [Fact]
        public void RemoveItem_ItemNotInInventory_LeavesItemsUnchanged()
        {
            var inventory = new Inventory();
            var sword = new Weapon("Sword", "A sharp blade", 10);
            var axe = new Weapon("Axe", "A heavy axe", 15);
            inventory.AddItem(sword);

            inventory.RemoveItem(axe);

            Assert.Equal(new[] { sword }, inventory.GetItems());
        }
    }
}
