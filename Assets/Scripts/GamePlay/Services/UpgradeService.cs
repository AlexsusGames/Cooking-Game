using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeService : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private List<GameObject> upgradableObjects;
    [Inject] private SteamAchievements achievements;

    private KitchenUpgradeProvider upgradeProvider;
    private readonly List<IUpgradable> allUpgradable = new();

    [Inject]
    private void Construct(KitchenUpgradeProvider provider)
    {
        upgradeProvider = provider;
        upgradeProvider.OnUpgraded += UpgradeKitchen;
    }

    public bool Has(InteractivePlaces type) => (int)type < upgradableObjects.Count;

    public void Add(GameObject gameObject)
    {
        upgradableObjects.Add(gameObject);
    }

    public void Load()
    {
        var upgradeMap = upgradeProvider.GetUpgrades();

        bool isFullUpgrade = true;

        foreach (var upgrade in upgradeMap.Keys)
        {
            if (upgrade == InteractivePlaces.WashStand)
                continue;

            if (upgradeMap[upgrade] < 3)
            {
                isFullUpgrade = false;
                break;
            }
        }

        if(isFullUpgrade)
        {
            achievements.TrySetAchievement(achievements.ACH_UPGRADE);
        }

        for (int i = 0; i < upgradableObjects.Count; i++)
        {
            upgradableObjects[i].TryGetComponent(out IUpgradable upgradable);
            upgradable.InteractiveTime = upgradeMap[upgradable.InteractiveType];
            allUpgradable.Add(upgradable);
        }
    }

    public void Save()
    {
        upgradeProvider.SaveData();
    }

    public void UpgradeKitchen(InteractivePlaces type, int level)
    {
        for (int i = 0; i < allUpgradable.Count; i++)
        {
            if (allUpgradable[i].InteractiveType == type)
            {
                allUpgradable[i].InteractiveTime = level;
            }
        }
    }
}
