using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateConfig/Recipe", fileName = "Recipe")]
public class RecipeConfig : SoLocalization
{
    public Sprite picture;
    public InteractivePlaces CookingPlace;
    public List<ProductConfig> Products = new();
    public GameObject Model;

    public string Name;
    public string Description;
    public float RarityIndex;

    public bool IsDrink;

    public int Price => GetCost();

    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            CachedKeys = new string[] { Name, Description };
        }

        return CachedKeys;
    }

    public override void Set(params string[] param)
    {
        Name = param[0];
        Description = param[1];
    }

    private int GetCost()
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
