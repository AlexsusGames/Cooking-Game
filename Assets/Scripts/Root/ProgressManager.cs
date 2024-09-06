using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject[] inventoryObjects;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private DishKeeper dishes;
    [SerializeField] private EndGameWindow endGameWindow;
    [SerializeField] private Bank bank;
    private readonly List<IInventoryData> allInventories = new();
    private readonly WorldState state = new();
    private readonly PeopleCounter peopleCounter = new();

    private void Awake()
    {
        for (int i = 0; i < inventoryObjects.Length; i++)
        {
            inventoryObjects[i].TryGetComponent(out IInventoryData data);
            allInventories.Add(data);
        }

        LoadInventories();
        LoadState();
    }

    private void LoadState()
    {
        var state = this.state.LoadState();

        lightSystem.Kitchen = state.KitchenLight;
        lightSystem.Bedroom = state.BedroomLight;

        dishes.CountOfDish = state.DishCount;
    }

    public bool EndDay()
    {
        TaxCounter.PeopleServed += 20;
        if(!lightSystem.IsOpen && !lightSystem.isDayTime)
        {
            SaveInventories();
            SaveState();
            bank.SaveMoney();
            peopleCounter.ChangeCount(TaxCounter.PeopleServed);


            endGameWindow.Open();
            return true;
        }
        return false;
    }

    private void SaveState()
    {
        WorldStateData data = new WorldStateData()
        {
            DishCount = dishes.CountOfDish,
            KitchenLight = lightSystem.Kitchen,
            BedroomLight = lightSystem.Bedroom
        };

        state.SaveState(data);
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
