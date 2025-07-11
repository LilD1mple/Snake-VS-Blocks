using DG.Tweening;
using SnakeVSBlocks.Extensions;
using SnakeVSBlocks.Services.Pause;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SnakeVSBlocks.UI.View
{
	public class PauseViewer : MonoBehaviour, IPauseable
	{
		[Header("Source")]
		[SerializeField] private CanvasGroup _pauseCanvasGroup;

		[Header("Settings")]
		[SerializeField] private float _duration;

		[Header("Callbacks")]
		[SerializeField] private UnityEvent _onPause;
		[SerializeField] private UnityEvent _onUnpause;

		[Inject] private readonly PauseService _pauseService;

		private Tween _currentTween;

		private const float PauseCanvasShowedValue = 1f;
		private const float PauseCanvasHidedValue = 0f;

		private void OnEnable()
		{
			_pauseService.Register(this);
		}

		private void OnDisable()
		{
			_pauseService.Unregister(this);
		}

		public void SetPause(bool isPaused)
		{
			if (isPaused)
				_pauseCanvasGroup.gameObject.SetActive(true);

			if (_currentTween.IsActive())
				_currentTween.Kill();

			_currentTween = _pauseCanvasGroup.DOFade(isPaused ? PauseCanvasShowedValue : PauseCanvasHidedValue, _duration)
				.OnComplete(() =>
				{
					if (isPaused == false)
						_pauseCanvasGroup.gameObject.SetActive(false);

					isPaused.CompareByTernaryOperation(() => _onPause?.Invoke(), () => _onUnpause?.Invoke());
				})
				.OnKill(() => _currentTween = null)
				.SetRecyclable(true);
		}
	}
}