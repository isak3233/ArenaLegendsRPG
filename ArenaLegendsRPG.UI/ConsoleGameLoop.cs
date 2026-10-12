using ArenaLegendsRPG.Core.GameFlow;
using ArenaLegendsRPG.Core.GameFlow.GameEvents;
using ArenaLegendsRPG.Core.GameFlow.Interfaces;
using ArenaLegendsRPG.Core.GameFlow.Menus;
using ArenaLegendsRPG.UI.Rendering;
using ArenaLegendsRPG.UI.Rendering.Input;

namespace ArenaLegendsRPG.UI;

public class ConsoleGameLoop
{
    private readonly IGameFlow _game;
    private readonly IGameEventRender _gameEventRender;
    private readonly IGameEventInputRouter _gameEventInputRouter;
    private readonly IConsoleIO _consoleIO;

    public ConsoleGameLoop(IGameFlow game, IGameEventRender gameEventRender, IGameEventInputRouter gameEventInputRouter, IConsoleIO consoleIO)
    {
        _game = game;
        _gameEventRender = gameEventRender;
        _gameEventInputRouter = gameEventInputRouter;
        _consoleIO = consoleIO;
    }

    public void Run()
    {
        while (_game.State != GameState.GameOver)
        {
            _consoleIO.Clear();
            var events = AskForMenuChoice();
            HandleEvents(events);
        }
    }


    private IReadOnlyList<GameEvent> AskForMenuChoice()
    {
        var actions = _game.GetAvailableActions();

        for (var i = 0; i < actions.Count; i++)
        {
            _consoleIO.WriteLine($"{i + 1}. {MenuTexts.For(_game.State, actions[i])}");
        }

        _consoleIO.Write("Skriv in ett alternativ: ");
        if (int.TryParse(_consoleIO.ReadLine(), out var choice) && choice >= 1 && choice <= actions.Count)
        {
            return _game.Choose(actions[choice - 1]);
        }
        _consoleIO.WriteLine("Ogiltigt val.");
        return Array.Empty<GameEvent>();
    }

    private void HandleEvents(IReadOnlyList<GameEvent> events)
    {
        var pendingEvents = new Queue<GameEvent>(events);

        while (pendingEvents.Count > 0)
        {
            var gameEvent = pendingEvents.Dequeue();

            var result = _gameEventInputRouter.Handle(gameEvent);

            if (result is not null)
            {
                IReadOnlyList<GameEvent> newEvents = result.Type switch
                {
                    InputType.String =>
                        _game.Submit((string)result.Value),
                    _ => Array.Empty<GameEvent>()
                };

                foreach (var newEvent in newEvents)
                {
                    pendingEvents.Enqueue(newEvent);
                }
            }
            else
            {
                _gameEventRender.PresentAll(new[] { gameEvent });
            }
        }
    }


}