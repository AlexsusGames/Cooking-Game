using UnityEngine;
using Zenject;

public class GamePlaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallObjects();
        InstallView();
        InstallServices();
    }

    private void InstallServices()
    {
        Container.Bind<KitchenUpgradeProvider>().FromNew().AsSingle();
        Container.Bind<DeviceDataProvider>().FromNew().AsSingle();
        Container.Bind<InventoryService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UpgradeService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KitchenStateSevice>().FromComponentInHierarchy().AsSingle();
    }

    private void InstallObjects()
    {
        Container.Bind<CoffeeKeeper>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CashTrigger>().FromComponentInHierarchy().AsSingle();
    }

    private void InstallView()
    {
        Container.Bind<InventoryGridView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputView>().FromComponentInHierarchy().AsSingle();
    }
}