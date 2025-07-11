using SnakeVSBlocks.Services.GameStart;
using Zenject;

namespace SnakeVSBlocks.Installers
{
	public class StartableServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindStartableService();
		}

		private void BindStartableService()
		{
			Container
				.Bind<StartableService>()
				.FromNew()
				.AsSingle()
				.NonLazy();
		}
	}
}
