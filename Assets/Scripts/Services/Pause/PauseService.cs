using System.Collections.Generic;

namespace SnakeVSBlocks.Services.Pause
{
	public sealed class PauseService : IPauseable
	{
		private readonly List<IPauseable> _pauseables = new();

		public bool IsPaused { get; private set; } = false;

		public void Register(IPauseable pauseable) => _pauseables.Add(pauseable);

		public void Unregister(IPauseable pauseable) => _pauseables.Remove(pauseable);

		public void SetPause(bool isPause)
		{
			IsPaused = isPause;

			for (int i = 0; i < _pauseables.Count; i++)
				_pauseables[i].SetPause(isPause);
		}
	}
}
