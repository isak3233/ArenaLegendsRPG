using System;
using System.Collections.Generic;
using System.Text;
using ArenaLegendsRPG.Core.Items.Interfaces;

namespace ArenaLegendsRPG.Core.Items
{
    public abstract class Item : IItem
    {
        public string Name { get; }
        public string Description { get; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }

}
