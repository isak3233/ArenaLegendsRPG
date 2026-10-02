using ArenaLegendsRPG.Core.RandomGen;

namespace ArenaLegendsRPG.Tests.Fakes;

internal class FakeRandomProvider : IRandomProvider
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
