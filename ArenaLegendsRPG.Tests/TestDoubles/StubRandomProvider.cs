using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.Tests.TestDoubles;

internal class StubRandomProvider : IRandomProvider
{
    private readonly Queue<int> _values;

    public StubRandomProvider(params int[] values)
    {
        _values = new Queue<int>(values);
    }
    public int Next(int min, int max)
    {
        return _values.Dequeue();
    }
}
