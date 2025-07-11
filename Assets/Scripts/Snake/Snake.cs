using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Signals;
using SnakeVSBlocks.Input;
using SnakeVSBlocks.Services.GameStart;
using SnakeVSBlocks.Services.Pause;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Snakes
{
	[RequireComponent(typeof(SnakeTrailGenerator))]
	public class Snake : MonoBehaviour, IStartable, IPauseable
	{
		[Header("Camera")]
		[SerializeField] private Camera _camera;

		[Header("Snake Settings")]
		[SerializeField] private int _startSnakeSize;
		[SerializeField] private float _snakeSpeed;
		[SerializeField] private float _tailSpringiness;

		private bool _isGameEnded = false;
		private bool _isGameStarted = false;
		private bool _isPaused = false;

		private SnakeHead _snakeHead;
		private EventBus _eventBus;
		private StartableService _startableService;
		private PauseService _pauseService;

		private IMouseInputService _mouseInputService;

		private SnakeTrailGenerator _snakeTrailGenerator;

		private List<SnakeSegment> _snakeSegments;

		public event Action<int> SnakeSegmentUpdated;

		public int SnakeLength => _snakeSegments.Count;

		private void Start()
		{
			_snakeTrailGenerator = GetComponent<SnakeTrailGenerator>();

			_snakeSegments = _snakeTrailGenerator.GenerateSnakeTrail(_startSnakeSize);

			SnakeSegmentUpdated?.Invoke(_snakeSegments.Count);
		}

		private void OnEnable()
		{
			_startableService.Register(this);

			_pauseService.Register(this);

			_snakeHead.BlockCollided += OnBlockCollided;
			_snakeHead.BonusCollected += OnBonusCollected;
		}

		private void OnDisable()
		{
			_startableService.Unregister(this);

			_pauseService.Unregister(this);

			_snakeHead.BlockCollided -= OnBlockCollided;
			_snakeHead.BonusCollected -= OnBonusCollected;
		}

		private void Update()
		{
			if (_isGameEnded || _isGameStarted == false || _isPaused)
				return;

			_snakeHead.transform.up = _mouseInputService.GetClickDirection(_snakeHead.transform.position, _camera);
		}

		private void FixedUpdate()
		{
			if (_isGameEnded || _isGameStarted == false || _isPaused)
				return;

			MoveSnake(_snakeHead.transform.position + _snakeHead.transform.up * _snakeSpeed * Time.fixedDeltaTime);
		}

		[Inject]
		public void Construct(IMouseInputService mouseInputService, SnakeHead snakeHead, EventBus eventBus, StartableService startableService, PauseService pauseService)
		{
			_mouseInputService = mouseInputService;

			_snakeHead = snakeHead;

			_eventBus = eventBus;

			_startableService = startableService;

			_pauseService = pauseService;
		}

		public void OnStart() => _isGameStarted = true;

		public void SetPause(bool isPause) => _isPaused = isPause;

		private void MoveSnake(Vector3 nextPosition)
		{
			Vector3 previousPosition = _snakeHead.transform.position;

			foreach (var segment in _snakeSegments)
			{
				Vector3 tempPosition = segment.transform.position;

				segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpringiness * Time.fixedDeltaTime);

				previousPosition = tempPosition;
			}

			_snakeHead.Move(nextPosition);
		}

		private void OnBlockCollided()
		{
			if (_snakeSegments.Count <= 0)
			{
				_eventBus.Raise(new GameOverSignal(this));

				_isGameEnded = true;

				return;
			}

			SnakeSegment lastSegment = _snakeSegments.Last();

			_snakeSegments.Remove(lastSegment);

			Destroy(lastSegment.gameObject);

			SnakeSegmentUpdated?.Invoke(_snakeSegments.Count);
		}

		private void OnBonusCollected(int bonusSize)
		{
			_snakeSegments.AddRange(_snakeTrailGenerator.GenerateSnakeTrail(bonusSize));

			SnakeSegmentUpdated?.Invoke(_snakeSegments.Count);
		}
	}
}
