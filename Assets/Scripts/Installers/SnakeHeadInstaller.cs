using SnakeVSBlocks.Snakes;
using UnityEngine;
using Zenject;

namespace SnakeVSBlocks.Installers
{
    public class SnakeHeadInstaller : MonoInstaller
    {
        [SerializeField] private SnakeHead _snakeHead; 

        public override void InstallBindings()
        {
            BindSnakeHead();
        }

        private void BindSnakeHead()
        {
            Container.Bind<SnakeHead>()
                .FromInstance(_snakeHead)
                .AsSingle()
                .NonLazy();
        }
    }
}