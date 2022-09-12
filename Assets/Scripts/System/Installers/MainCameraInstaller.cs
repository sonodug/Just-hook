using UnityEngine;
using Zenject;

namespace System.Installers
{
    public class MainCameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle().NonLazy();
        }
    }
}
