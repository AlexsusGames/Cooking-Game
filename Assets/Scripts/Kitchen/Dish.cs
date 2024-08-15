using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    private RecipeConfig config;

    public void SetFood(RecipeConfig config)
    {
        Instantiate(config.Model, transform);
    }

    public RecipeConfig GetFood()
    {
        return config;
    }
}
