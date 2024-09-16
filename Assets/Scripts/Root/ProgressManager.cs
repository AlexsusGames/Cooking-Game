using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryObjects;
    [SerializeField] private GameObject[] upgradableObjects;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private DishKeeper dishes;
    [SerializeField] private EndGameWindow endGameWindow;
    [SerializeField] private Bank bank;
    [SerializeField] private Keeper foodKeeper;
    [SerializeField] private Keeper drinkKeeper;
    [SerializeField] private TaxManager taxes;
    [SerializeField] private KitchenUpgrade upgrades;

    private readonly List<IInventoryData> allInventories = new();
    private readonly List<IUpgradable> allUpgradable = new();
    private readonly WorldState state = new();
    private readonly PeopleCounter peopleCounter = new();
    private readonly StatsProvider stats = new();
    private KitchenUpgradeProvider kitchenUpgradeProvider;

    private void Awake()
    {
        kitchenUpgradeProvider = new();
        upgrades.Init(kitchenUpgradeProvider);
        var upgr = kitchenUpgradeProvider.GetUpgrades();

        kitchenUpgradeProvider.OnUpgraded += UpgradeKitchen;

        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].TryGetComponent(out IInventoryData data);
            allInventories.Add(data);
        }

        for (int i = 0; i < upgradableObjects.Length; i++)
        {
            upgradableObjects[i].TryGetComponent(out IUpgradable upgradable);
            upgradable.InteractiveTime = upgr[upgradable.InteractiveType];
            allUpgradable.Add(upgradable);
        }

        TaxCounter.Reset();

        LoadInventories();
        LoadState();
    }



    private void LoadState()
    {
        var state = this.state.LoadState();

        lightSystem.Kitchen = state.KitchenLight;
        lightSystem.Bedroom = state.BedroomLight;

        dishes.CountOfDish = state.DishCount;

        foodKeeper.CountOfFood = state.RemainedFood;
        drinkKeeper.CountOfFood = state.RemainedDrinks;

        taxes.Load();
    }

    public bool EndDay()
    {
        if(!lightSystem.IsOpen && !lightSystem.isDayTime)
        {
            SaveInventories();
            SaveState();
            bank.SaveMoney();
            peopleCounter.ChangeCount(TaxCounter.PeopleServed);

            taxes.Change(TaxCounter.GetTaxes());
            taxes.Save();
            kitchenUpgradeProvider.SaveData();

            UpdateStats();
            endGameWindow.Open();
            return true;
        }
        return false;
    }

    private void UpdateStats()
    {
        DayStatsData dayStatsData = new DayStatsData()
        {
            MoneyEarned = TaxCounter.Income,
            MoneySpended = bank.GetLoses(),
            PeopleServed = TaxCounter.PeopleServed
        };

        stats.AddDayToStats(dayStatsData);
    }

    private void SaveState()
    {
        WorldStateData data = new WorldStateData()
        {
            DishCount = dishes.CountOfDish,
            KitchenLight = lightSystem.Kitchen,
            BedroomLight = lightSystem.Bedroom,
            RemainedFood = foodKeeper.CountOfFood,
            RemainedDrinks = drinkKeeper.CountOfFood,
        };

        state.SaveState(data);
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

    private void LoadInventories()
    {
        for (int i = 0; i < allInventories.Count; i++)
        {
            allInventories[i].Load();
        }
    }

    private void SaveInventories()
    {
        for (int i = 0; i < allInventories.Count; i++)
        {
            allInventories[i].Save();
        }
    }
}
