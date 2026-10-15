
using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.Items
{
    public abstract class Item : IItem
    {
        public string Name { get; }
        public string Description { get; }
        public ItemRarity Rarity { get; }

        public Item(string name, string description, ItemRarity rarity = ItemRarity.Common)
        {
            Name = name;
            Description = description;
            Rarity = rarity;
        }
    }

}
