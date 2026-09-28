using System;
using System.Collections.Generic;
using System.Text;
using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.Tests;

public class FakeRandomProvider : IRandomProvider
{
    private readonly Queue<int> _values;

    public FakeRandomProvider(params int[] values)
    {
        _values = new Queue<int>(values);
    }
    public int Next(int min, int max)
    {
        return _values.Dequeue();
    }
}
