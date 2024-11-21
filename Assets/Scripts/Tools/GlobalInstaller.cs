using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private List<TMP_FontAsset> standartFonts;
    [SerializeField] private List<TMP_FontAsset> bookFonts;
    private SceneLoader sceneLoader = new();
    private SoundDataProvider soundDataProvider = new();
    public override void InstallBindings()
    {
        Container.Bind<SceneLoader>().FromInstance(sceneLoader).AsSingle();
        Container.Bind<SoundDataProvider>().FromInstance(soundDataProvider).AsSingle();
        var languageChanger = new LanguageChanger(standartFonts, bookFonts);
        Container.Bind<LanguageChanger>().FromInstance(languageChanger).AsSingle();
        Container.Bind<SteamAchievements>().AsSingle();
    }
}