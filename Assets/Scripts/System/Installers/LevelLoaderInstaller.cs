using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class LevelLoaderInstaller : MonoInstaller
{
    [SerializeField] private LevelLoader _levelLoader;

    public override void InstallBindings()
    {
        Container.Bind<LevelLoader>().FromInstance(_levelLoader).AsSingle().NonLazy();
    }
}
