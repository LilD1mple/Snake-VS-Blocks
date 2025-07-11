using SnakeVSBlocks.Input;
using Zenject;

namespace SnakeVSBlocks.Installers
{
	public class MouseInputServiceInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindMouseInput();
		}

		private void BindMouseInput()
		{
			Container
				.Bind<IMouseInputService>()
				.To<SnakeInputService>()
				.FromNew()
				.AsSingle();
		}
	}
}