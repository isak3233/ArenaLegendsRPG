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
    private readonly GameEventRender _gameEventRender;
    private readonly GameEventInputRouter _gameEventInputRouter;

    public ConsoleGameLoop(IGameFlow game, GameEventRender gameEventRender,  GameEventInputRouter gameEventInputRouter)
    {
        _game = game;
        _gameEventRender = gameEventRender;
        _gameEventInputRouter =  gameEventInputRouter;
    }

    public void Run()
    {
        while (_game.State != GameState.GameOver)
        {
            Console.Clear();
            var events = AskForMenuChoice();
            HandleEvents(events);
        }
    }
    

    private IReadOnlyList<GameEvent> AskForMenuChoice()
    {
        var actions = _game.GetAvailableActions();

        for (var i = 0; i < actions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {MenuTexts.For(_game.State, actions[i])}");
        }


        if (int.TryParse(Console.ReadLine(), out var choice) && choice >= 1 && choice <= actions.Count)
        {
            
            return _game.Choose(actions[choice - 1]);
        }
        Console.WriteLine("Ogiltigt val.");
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