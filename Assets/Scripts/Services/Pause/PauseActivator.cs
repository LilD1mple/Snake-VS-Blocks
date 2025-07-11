using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Events.Signals;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Services.Pause
{
	public class PauseActivator : MonoBehaviour, IEventReceiver<GameOverSignal>, IEventReceiver<LevelCompleteSignal>
	{
		private bool _isPaused = false;
		private bool _canShowPause = true;

		private EventBus _eventBus;
		private PauseService _pauseService;

		UniqueId IBaseEventReceiver.Id => new();

		private void OnEnable()
		{
			_eventBus.Subscribe(this as IEventReceiver<GameOverSignal>);

			_eventBus.Subscribe(this as IEventReceiver<LevelCompleteSignal>);
		}

		private void OnDisable()
		{
			_eventBus.Unsubscribe(this as IEventReceiver<GameOverSignal>);

			_eventBus.Unsubscribe(this as IEventReceiver<LevelCompleteSignal>);
		}

		private void OnApplicationFocus(bool focus)
		{
			if (focus == false && _isPaused == false)
				RaiseSwitchPause();
		}

		[Inject]
		public void Construct(PauseService pauseService, EventBus eventBus)
		{
			_eventBus = eventBus;

			_pauseService = pauseService;
		}

		public void RaiseSwitchPause()
		{
			if (_canShowPause == false)
				return;

			_isPaused = !_isPaused;

			_pauseService.SetPause(_isPaused);
		}

		void IEventReceiver<GameOverSignal>.OnEvent(GameOverSignal @event) => _canShowPause = false;

		void IEventReceiver<LevelCompleteSignal>.OnEvent(LevelCompleteSignal @event) => _canShowPause = false;
	}
}