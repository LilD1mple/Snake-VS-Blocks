using SnakeVSBlocks.Services.GameStart;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace SnakeVSBlocks.Bootstrap
{
	public class Bootstrap : MonoBehaviour
	{
		[SerializeField] private UnityEvent _onGameStartedDisposeEvent;

		private StartableService _startableService;

		[Inject]
		public void Construct(StartableService startableService)
		{
			_startableService = startableService;
		}

		public void OnStartButtonClicked()
		{
			StartGame();
		}

		private void StartGame()
		{
			_startableService.OnStart();

			RaiseDisposeEvent();
		}

		private void RaiseDisposeEvent()
		{
			_onGameStartedDisposeEvent?.Invoke();
		}
	}
}