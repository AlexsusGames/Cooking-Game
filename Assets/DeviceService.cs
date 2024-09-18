using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DeviceService : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private Transform parent;

    private DeviceConfig[] deviceConfigs;
    private DeviceDataProvider dataProvider;

    [Inject] private DiContainer container;
    [Inject] private InventoryService inventoryService;
    [Inject] private UpgradeService upgradeService;

    public void Load()
    {
        dataProvider = new();
        deviceConfigs = Resources.LoadAll<DeviceConfig>("Devices");

        var device = deviceConfigs[0];

        AddObject(device); // test

        Setup();
    }

    public void Setup()
    {
        for (int i = 0; i < deviceConfigs.Length; i++)
        {
            var device = deviceConfigs[i];

            if(dataProvider.Has(device.Name))
            {
                var position = dataProvider.GetPosition(device.Name);
                Spawn(device.Prefab, position);
            }
        }
    }

    public void AddObject(DeviceConfig config, Vector3 position = new Vector3())
    {
        if(!dataProvider.Has(config.Name))
        {
            if(config is UnmovableDeviceConfig unmovable)
            {
                position = unmovable.StandartPosition;
            }

            dataProvider.AddDevice(config.Name, position);
            Spawn(config.Prefab, position);
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
    }
}
