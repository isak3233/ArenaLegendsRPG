using ArenaLegendsRPG.UI;

namespace ArenaLegendsRPG.Tests.TestDoubles;

public class FakeConsoleIO : IConsoleIO
{
    private readonly Queue<string?> _input = new();

    public List<string> Output { get; } = new();

    public int ClearCount { get; private set; }

    public void SetInput(params string?[] inputs)
    {
        foreach (var input in inputs)
        {
            _input.Enqueue(input);
        }
    }

    public string? ReadLine()
    {
        return _input.Count > 0 ? _input.Dequeue() : null;
    }

    public void ReadKey()
    {

    }

    public void WriteLine(string message)
    {
        Output.Add(message);
    }

    public void Write(string message)
    {
        Output.Add(message);
    }

    public void Clear()
    {
        ClearCount++;
    }
}