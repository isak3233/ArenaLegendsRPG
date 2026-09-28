using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaLegendsRPG.Core.RandomGen
{
    public interface IRandomProvider
    {
        int Next(int min, int max);
    }
}
