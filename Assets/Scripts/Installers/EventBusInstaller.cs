using SnakeVSBlocks.Events;
using Zenject;

namespace SnakeVSBlocks.Installers
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEventBus();
        }

        private void BindEventBus()
        {
            Container.Bind<EventBus>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}