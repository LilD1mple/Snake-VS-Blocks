using SnakeVSBlocks.Services.Pause;
using Zenject;

namespace SnakeVSBlocks.Installers
{
	public class PauseServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindPauseService();
		}

		private void BindPauseService()
		{
			Container
				.Bind<PauseService>()
				.FromNew()
				.AsSingle()
				.NonLazy();
		}
	}
}