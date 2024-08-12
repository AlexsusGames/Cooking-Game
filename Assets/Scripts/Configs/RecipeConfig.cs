using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateConfig/Recipe", fileName = "Recipe")]
public class RecipeConfig : ScriptableObject
{
    public List<ProductConfig> Products = new();
    public GameObject Model;

    public string Name;
    public float RarityIndex;

    public int GetCost()
    {
        int cost = 0;
        for (int i = 0; i < Products.Count; i++)
        {
            cost += Products[i].ProductCost;
        }

        cost = (int)(cost * RarityIndex);

        return cost;
    }
}
