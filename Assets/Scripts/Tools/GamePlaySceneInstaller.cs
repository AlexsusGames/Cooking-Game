using UnityEngine;
using Zenject;

public class GamePlaySceneInstaller : MonoInstaller
{
    [SerializeField] private ProgressManager progressManager;
    public override void InstallBindings()
    {
        InstallObjects();
        InstallView();
        InstallServices();
    }

    private void InstallServices()
    {
        Container.Bind<KitchenUpgradeProvider>().AsSingle();
        Container.Bind<DeviceDataProvider>().AsSingle();
        Container.Bind<QuestHandler>().AsSingle();
        Container.Bind<QuestConfigFinder>().AsSingle();
        Container.Bind<WindowController>().AsSingle();
        Container.Bind<TaxManager>().FromComponentInHierarchy().AsSingle();
    }

    private void InstallObjects()
    {
        Container.Bind<ProgressManager>().FromInstance(progressManager).AsSingle();
        Container.Bind<CoffeeKeeper>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CashTrigger>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InteractSound>().FromComponentInHierarchy().AsSingle();
    }

    private void InstallView()
    {
        Container.Bind<InventoryGridView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputView>().FromComponentInHierarchy().AsSingle();
    }
}