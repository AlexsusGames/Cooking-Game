using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenStateSevice : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private DishKeeper dishes;
    [SerializeField] private List<Keeper> keepers;

    private readonly WorldState state = new();

    public void Add(Keeper keeper) => keepers.Add(keeper);

    public void Load()
    {
        var state = this.state.LoadState();

        lightSystem.Kitchen = state.KitchenLight;
        lightSystem.Bedroom = state.BedroomLight;

        dishes.CountOfDish = state.DishCount;

        state.Keepers = new List<int>(new int[2]);

        for (int i = 0; i < keepers.Count; i++)
        {
            keepers[i].CountOfFood = state.Keepers[i];
        }
    }

    public void Save()
    {
        List<int> remainingFood = new();

        for (int i = 0; i < remainingFood.Count; i++)
        {
            remainingFood.Add(keepers[i].CountOfFood);
        }

        WorldStateData data = new WorldStateData()
        {
            DishCount = dishes.CountOfDish,
            KitchenLight = lightSystem.Kitchen,
            BedroomLight = lightSystem.Bedroom,
            Keepers = remainingFood
        };

        state.SaveState(data);
    }
}
