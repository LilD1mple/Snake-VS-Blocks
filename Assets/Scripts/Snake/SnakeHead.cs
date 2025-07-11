using SnakeVSBlocks.Blocks;
using SnakeVSBlocks.Bonuses;
using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Events.Signals;
using SnakeVSBlocks.Services.GameStart;
using SnakeVSBlocks.Services.Pause;
using System;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Snakes
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class SnakeHead : MonoBehaviour, IEventReceiver<GameOverSignal>, IStartable, IPauseable
	{
		private bool _isGameEnded = false;
		private bool _isGameStarted = false;
		private bool _isPaused = false;

		private Rigidbody2D _rigidbody2D;

		private EventBus _eventBus;
		private StartableService _startableService;
		private PauseService _pauseService;

		public event Action BlockCollided;

		public event Action<int> BonusCollected;

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

		private void Start()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		private void OnCollisionStay2D(Collision2D collision)
		{
			if (collision.collider.TryGetComponent(out Block block))
			{
				BlockCollided?.Invoke();

				block.Fill();
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.TryGetComponent(out Bonus bonus))
			{
				BonusCollected?.Invoke(bonus.BonusScore);

				Destroy(bonus.gameObject);
			}
		}

		[Inject]
		public void Construct(EventBus eventBus, StartableService startableService, PauseService pauseService)
		{
			_eventBus = eventBus;

			_startableService = startableService;

			_pauseService = pauseService;
		}

		public void Move(Vector3 newPosition)
		{
			if (_isGameEnded || _isGameStarted == false)
				return;

			_rigidbody2D.MovePosition(newPosition);
		}

		public void OnStart()
		{
			_isGameStarted = true;

			_rigidbody2D.isKinematic = false;

			_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
		}

		public void SetPause(bool isPause)
		{
			_isPaused = isPause;

			_rigidbody2D.simulated = isPause == false;
		}

		void IEventReceiver<GameOverSignal>.OnEvent(GameOverSignal @event)
		{
			_isGameEnded = true;

			enabled = false;
		}
	}
}
