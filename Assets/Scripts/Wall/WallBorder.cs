using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Events.Signals;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Walls
{
    public class WallBorder : MonoBehaviour, IEventReceiver<GameOverSignal>
    {
        [SerializeField] private Transform _target;

        [Inject] private readonly EventBus _eventBus;

        UniqueId IBaseEventReceiver.Id => new();

        private void OnEnable()
        {
            _eventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe(this);
        }

        private void Update()
        {
            transform.position = new Vector3(transform.position.x, _target.position.y, transform.position.z);
        }

        void IEventReceiver<GameOverSignal>.OnEvent(GameOverSignal @event)
        {
            Destroy(this);
        }
    }
}
