namespace ArenaLegendsRPG.UI.Rendering.Input;

public class InputHelper : IInputHelper
{
    private readonly IConsoleIO _consoleIO;
    public InputHelper(IConsoleIO consoleIO)
    {
        _consoleIO =  consoleIO;
    }
    public string GetString(string inputString)
    {
        _consoleIO.Write(inputString);
        var input = _consoleIO.ReadLine() ?? string.Empty;

        return input;
    }
}