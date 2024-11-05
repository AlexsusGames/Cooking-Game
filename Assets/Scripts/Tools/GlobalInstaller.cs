using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    private SceneLoader sceneLoader = new();
    private SoundDataProvider soundDataProvider = new();
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
        Container.Bind<SoundDataProvider>().FromInstance(soundDataProvider).AsSingle();
        Container.Bind<LanguageChanger>().AsSingle();
    }
}