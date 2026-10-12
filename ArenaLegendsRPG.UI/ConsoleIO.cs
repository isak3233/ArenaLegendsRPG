namespace ArenaLegendsRPG.UI;

public class ConsoleIO : IConsoleIO
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void ReadKey()
    {
        Console.ReadKey();
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public void Clear()
    {
        Console.Clear();
    }
}