using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WorldSettings : MonoBehaviour
{
    [SerializeField] private GameObject girlObject;
    [SerializeField] private NPCManager npcManager;

    [SerializeField] private DeviceConfig[] firstConfiguration;
    [SerializeField] private DeviceConfig[] secondConfiguration;

    [Inject] private DeviceDataProvider deviceData;

    public void Init(PlayerChanger playerChanger, bool isHasParent)
    {
        if(!isHasParent)
        {
            Switch(firstConfiguration, secondConfiguration);
            ChangeGirlEnable(false);
        }
        else Switch(secondConfiguration, firstConfiguration);

        playerChanger.SetPlayer(isHasParent);
    }

    public void ChangeGirlEnable(bool value)
    {
        girlObject.SetActive(value);
        npcManager.enabled = value;
    }

    private void Switch(DeviceConfig[] deviceToRemove, DeviceConfig[] deviceToAdd)
    {
        for (int i = 0; i < deviceToRemove.Length; i++)
        {
            if (deviceData.Has(deviceToRemove[i].name))
            {
                deviceData.RemovedDevice(deviceToRemove[i].name);
            }
        }

        for (int i = 0; i < deviceToAdd.Length; i++)
        {
            if (!deviceData.Has(deviceToAdd[i].name) && deviceToAdd[i] is UnmovableDeviceConfig config)
            {
                deviceData.AddDevice(deviceToAdd[i].name, config.StandartPosition);
            }
        }
    }
}
