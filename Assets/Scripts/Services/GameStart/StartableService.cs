using System.Collections.Generic;

namespace SnakeVSBlocks.Services.GameStart
{
	public sealed class StartableService : IStartable
	{
		private readonly List<IStartable> _startables = new();

		public void Register(IStartable startable) => _startables.Add(startable);

		public void Unregister(IStartable startable) => _startables.Remove(startable);

		public void OnStart()
		{
			for (int i = 0; i < _startables.Count; i++)
				_startables[i].OnStart();
		}
	}
}
