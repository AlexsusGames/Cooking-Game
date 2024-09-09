using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour, IFood
{
    [SerializeField] private RecipeConfig config;

    private void Awake()
    {
        if(config != null)
        {
            SetFood(config);
        }
    }

    public void SetFood(RecipeConfig config)
    {
        this.config = config;
        Instantiate(config.Model, transform);
    }

    public RecipeConfig GetFood()
    {
        return config;
    }
}
