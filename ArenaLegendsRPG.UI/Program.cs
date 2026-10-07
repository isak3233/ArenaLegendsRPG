using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameServices;

namespace ArenaLegendsRPG.UI;

class Program
{
    static void Main(string[] args)
    {
        var session = new GameSession();
        var characterCreationService = new CharacterCreationService();
        var factory = new ScreenFactory(session, characterCreationService);
        var game = new GameFlow(factory);

        Console.WriteLine("Hello, World!");
    }
}