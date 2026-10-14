using System.Globalization;

namespace ArenaLegendsRPG.UI.Rendering.Input;

public class InputHelper : IInputHelper
{
    private readonly IConsoleIO _consoleIO;
    public InputHelper(IConsoleIO consoleIO)
    {
        _consoleIO = consoleIO;
    }
    public string GetString(string inputString)
    {
        _consoleIO.Write(inputString);
        var input = _consoleIO.ReadLine() ?? string.Empty;

        return input;
    }

    public int GetInt(string inputString)
    {
        while (true)
        {
            _consoleIO.Write(inputString);

            var input = _consoleIO.ReadLine();

            if (int.TryParse(input.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var number))
            {
                return number;
            }

            _consoleIO.WriteLine("Ogiltigt tal, försök igen.");
        }
    }
}