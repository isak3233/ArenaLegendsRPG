using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaLegendsRPG.Core.Items
{
    public class Item
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
