using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Events.Signals;
using SnakeVSBlocks.Save;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Levels
{
	public class LevelIncrementer : MonoBehaviour, IEventReceiver<LevelCompleteSignal>
	{
		[Inject] private readonly EventBus _eventBus;

		private const string ProgressString = "LevelConfiguration";

		private const int DefaultLevel = 0;

		UniqueId IBaseEventReceiver.Id => new();

		private void OnEnable()
		{
			_eventBus.Subscribe(this);
		}

		private void OnDisable()
		{
			_eventBus.Unsubscribe(this);
		}

		void IEventReceiver<LevelCompleteSignal>.OnEvent(LevelCompleteSignal @event)
		{
			IncrementLevel();
		}

		private void IncrementLevel()
		{
			int currentLevel = SaveUtility.LoadValue(ProgressString, DefaultLevel);

			++currentLevel;

			SaveUtility.SaveValue(ProgressString, currentLevel);
		}
	}
}