using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NextLevelZoneInstaller : MonoInstaller
{
    [SerializeField] private NextLevelZone _nextLevelZone;

    public override void InstallBindings()
    {
        Container.Bind<NextLevelZone>().FromInstance(_nextLevelZone).AsSingle().NonLazy();
    }
}
