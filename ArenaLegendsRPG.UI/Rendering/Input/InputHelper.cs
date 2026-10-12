namespace ArenaLegendsRPG.UI.Rendering.Input;

public class InputHelper
{
    public string GetString(string inputString)
    {
        Console.Write(inputString);
        var input = Console.ReadLine() ?? string.Empty;

        return input;
    }
}