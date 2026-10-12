namespace ArenaLegendsRPG.UI;

public interface IConsoleIO
{
    string? ReadLine();
    void ReadKey();
    void WriteLine(string message);
    void Write(string message);
    void Clear();

}