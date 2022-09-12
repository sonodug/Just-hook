using UnityEngine;
using Zenject;

namespace System.Installers
{
    public class LevelLoaderInstaller : MonoInstaller
    {
        [SerializeField] private LevelLoader _levelLoader;

        public override void InstallBindings()
        {
            Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle().NonLazy();
        }
    }
}
