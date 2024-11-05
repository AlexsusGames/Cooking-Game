using UnityEngine;
using Zenject;

public class ServiceInstaller : MonoInstaller
{
    [SerializeField] private InventoryService inventoryService;
    [SerializeField] private UpgradeService upgradeService;
    [SerializeField] private KitchenStateSevice kitchenStateSevice;
    [SerializeField] private CompletedDialogsManager completedDialogsManager;

    public override void InstallBindings()
    {
        Container.Bind<InventoryService>().FromInstance(inventoryService).AsSingle();
        Container.Bind<UpgradeService>().FromInstance(upgradeService).AsSingle();
        Container.Bind<KitchenStateSevice>().FromInstance(kitchenStateSevice).AsSingle();
        Container.Bind<FamilyStateManager>().AsSingle();
        Container.Bind<CompletedDialogsManager>().FromInstance(completedDialogsManager).AsSingle();
    }
}