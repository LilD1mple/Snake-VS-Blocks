using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Snakes;

namespace SnakeVSBlocks.Events.Signals
{
    public readonly struct GameOverSignal : IEvent
    {
        public Snake Snake { get; }

        public GameOverSignal(Snake snake)
        {
            Snake = snake;
        }
    }
}
