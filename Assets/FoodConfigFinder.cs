using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FoodConfigFinder 
{
    private Dictionary<string, ProductConfig> productsMap;
    private RecipeConfig[] recipes;

    private void CreateProductMap()
    {
        if (productsMap == null)
        {
            productsMap = new();
            var allProducts = Resources.LoadAll<ProductConfig>("Products");

            for (int i = 0; i < allProducts.Length; i++)
            {
                productsMap[allProducts[i].ProductName] = allProducts[i];
            }
        }
    }

    public ProductConfig GetProductByName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            CreateProductMap();

            return productsMap[name];
        }
        return null;
    }

    public RecipeConfig CookingFood(List<ProductConfig> products, InteractivePlaces place)
    {
        if (recipes == null) recipes = Resources.LoadAll<RecipeConfig>("Recipes");

        var remainingRecipes = recipes.ToList();

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i].Products.Count != products.Count || recipes[i].CookingPlace != place)
            {
                remainingRecipes.Remove(recipes[i]);
            }

            for (int j = 0; j < products.Count; j++)
            {
                if (!recipes[i].Products.Contains(products[j]))
                {
                    remainingRecipes.Remove(recipes[i]);
                }
            }
        }
        if (remainingRecipes.Count == 1) return remainingRecipes[0];

        else return null;
    }

    public List<ProductConfig> GetProductsByName(List<string> names)
    {
        var list = new List<ProductConfig>();
        CreateProductMap();

        for (int i = 0; i < names.Count; i++)
        {
            list.Add(productsMap[names[i]]);
        }

        return list;
    }

}
