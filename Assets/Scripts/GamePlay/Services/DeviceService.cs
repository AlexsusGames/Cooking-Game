using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeviceService : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private Transform parent;

    private DeviceConfig[] deviceConfigs;
    [Inject] private DeviceDataProvider dataProvider;

    [Inject] private DiContainer container;

    [Inject] private InventoryService inventoryService;
    [Inject] private UpgradeService upgradeService;
    [Inject] private KitchenStateSevice kitchenStateSevice;

    public void Load()
    {
        deviceConfigs = Resources.LoadAll<DeviceConfig>("Localization/Devices");

        Setup();
    }

    public void Setup()
    {
        for (int i = 0; i < deviceConfigs.Length; i++)
        {
            var device = deviceConfigs[i];

            if(dataProvider.Has(device.name))
            {
                var position = dataProvider.GetPosition(device.name);
                Spawn(device.Prefab, position);
            }
        }
    }

    public void Save() => dataProvider.SaveData();

    private void Spawn(GameObject gameObject, Vector3 position)
    { 
        var obj = container.InstantiatePrefab(gameObject, parent);
        obj.transform.localPosition = position;

        if(obj.TryGetComponent(out IInventoryData _))
        {
            inventoryService.Add(obj);
        }

        if (obj.TryGetComponent(out IUpgradable _))
        {
            upgradeService.Add(obj);
        }

        if(obj.TryGetComponent(out Keeper keeper))
        {
            kitchenStateSevice.Add(keeper);
        }
    }
}
