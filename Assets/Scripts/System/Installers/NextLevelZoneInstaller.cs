using Progress_Zones;
using UnityEngine;
using Zenject;

namespace System.Installers
{
    public class NextLevelZoneInstaller : MonoInstaller
    {
        [SerializeField] private NextLevelZone _nextLevelZone;
        
        public override void InstallBindings()
        {
            Container.Bind<NextLevelZone>().FromInstance(_nextLevelZone).AsSingle().NonLazy();
        }
    }
}
