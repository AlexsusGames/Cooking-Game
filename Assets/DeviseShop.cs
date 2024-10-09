using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DeviseShop : MonoBehaviour
{
    [SerializeField] private GameObject shopViewPrefab;
    [SerializeField] private Transform[] points;
    [SerializeField] private Bank bank;
    [SerializeField] private TMP_Text itemInfo;
    [Inject] private DeviceDataProvider dataProvider;
    private DeviceConfig[] devices;
    private List<DeviceShopView> views = new();

    private void Awake() => devices = Resources.LoadAll<DeviceConfig>("Devices");

    private void Start()
    {
        for (int i = 0; i < devices.Length; i++)
        {
            int index = i;
            var obj = Instantiate(shopViewPrefab, points[index]);
            obj.TryGetComponent(out DeviceShopView shopView);
            shopView.SetData(devices[index], dataProvider.Has(devices[index].name));
            views.Add(shopView);

            shopView.GetButton().onClick.AddListener(() => BuyDevice(devices[index]));
            shopView.TryGetComponent(out Button button);
            button.onClick.AddListener(() => itemInfo.text = devices[index].Describtion);
        }
    }

    private void UpdateView()
    {
        for (int i = 0; i < views.Count; i++)
        {
            views[i].SetData(devices[i], dataProvider.Has(devices[i].name));
        }
    }

    private void BuyDevice(DeviceConfig config)
    {
        if (bank.Has(config.Price) && !dataProvider.Has(config.name))
        {
            bank.Change(-config.Price);
            Vector3 pos = Vector3.zero;

            if (config is UnmovableDeviceConfig unmovable)
            {
                pos = unmovable.StandartPosition;
            }

            dataProvider.AddDevice(config.name, pos);

            UpdateView();
        }
    }
}
