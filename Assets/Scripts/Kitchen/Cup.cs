using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour, IFood
{
    [SerializeField] private RecipeConfig config;

    public void SetFood(RecipeConfig config)
    {
        this.config = config;
    }

    public RecipeConfig GetFood()
    {
        return config;
    }
}
