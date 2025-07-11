using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Signals;
using SnakeVSBlocks.Snakes;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.FinishLine
{
    public class Finish : MonoBehaviour
    {
        [Inject] private readonly EventBus _eventBus;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Snake snake = collision.GetComponentInParent<Snake>();

            if (snake != null)
                _eventBus.Raise(new LevelCompleteSignal());
        }
    }
}
