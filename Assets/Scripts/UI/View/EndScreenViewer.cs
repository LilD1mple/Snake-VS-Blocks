using DG.Tweening;
using SnakeVSBlocks.Events;
using SnakeVSBlocks.Events.Interfaces;
using SnakeVSBlocks.Events.Signals;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.UI.View
{
	public class EndScreenViewer : MonoBehaviour, IEventReceiver<GameOverSignal>, IEventReceiver<LevelCompleteSignal>
	{
		[Header("Lose Screen")]
		[SerializeField] private Canvas _loseCanvas;
		[SerializeField] private CanvasGroup _loseCanvasGroup;

		[Header("Complete Screen")]
		[SerializeField] private Canvas _completeCanvas;
		[SerializeField] private CanvasGroup _completeCanvasGroup;

		[Header("Animation")]
		[SerializeField] private float _duration;

		[Inject] private readonly EventBus _eventBus;

		private const float CanvasEndValue = 1f;

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

		private void OnDestroy()
		{
			DOTween.KillAll();
		}

		private void AnimateCanvas(Canvas canvas, CanvasGroup canvasGroup)
		{
			canvas.enabled = true;

			canvasGroup.DOFade(CanvasEndValue, _duration);
		}

		void IEventReceiver<GameOverSignal>.OnEvent(GameOverSignal @event) => AnimateCanvas(_loseCanvas, _loseCanvasGroup);

		void IEventReceiver<LevelCompleteSignal>.OnEvent(LevelCompleteSignal @event) => AnimateCanvas(_completeCanvas, _completeCanvasGroup);
	}
}
