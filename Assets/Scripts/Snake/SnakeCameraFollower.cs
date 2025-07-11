using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Events.Signals;
using SnakeVSBlocks.Services.GameStart;
using SnakeVSBlocks.Services.Pause;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Snakes
{
	public class SnakeCameraFollower : MonoBehaviour, IEventReceiver<GameOverSignal>, IStartable, IPauseable
	{
		[SerializeField] private float _followSpeed;
		[SerializeField] private float _followOffsetY;

		private bool _isGameStarted = false;
		private bool _isPaused = false;

		private EventBus _eventBus;
		private SnakeHead _target;
		private StartableService _startableService;
		private PauseService _pauseService;

		UniqueId IBaseEventReceiver.Id => new();

		private void OnEnable()
		{
			_startableService.Register(this);

			_pauseService.Register(this);

			_eventBus.Subscribe(this);
		}

		private void OnDisable()
		{
			_startableService.Unregister(this);

			_pauseService.Unregister(this);

			_eventBus.Unsubscribe(this);
		}

		private void FixedUpdate()
		{
			if (_isGameStarted == false || _isPaused)
				return;

			FollowCamera();
		}

		[Inject]
		public void Construct(EventBus eventBus, SnakeHead snakeHead, StartableService startableService, PauseService pauseService)
		{
			_eventBus = eventBus;

			_target = snakeHead;

			_startableService = startableService;

			_pauseService = pauseService;
		}

		public void OnStart() => _isGameStarted = true;

		public void SetPause(bool isPause) => _isPaused = isPause;

		private void FollowCamera() => transform.position = Vector3.Lerp(transform.position, GetTargetPosition(), _followSpeed * Time.fixedDeltaTime);

		private Vector3 GetTargetPosition() => new Vector3(transform.position.x, _target.transform.position.y + _followOffsetY, transform.position.z);

		void IEventReceiver<GameOverSignal>.OnEvent(GameOverSignal @event) => enabled = false;
	}
}
