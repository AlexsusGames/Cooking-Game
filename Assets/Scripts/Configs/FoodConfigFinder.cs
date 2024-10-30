using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FoodConfigFinder 
{
    private Dictionary<string, ProductConfig> productsMap;
    private Dictionary<string, RecipeConfig> recipeMap;

    private KnownRecipes knownRecipes = new();
    public int RecipeCount => recipeMap.Count;

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

    public RecipeConfig GetRecipeByName(string name)
    {
        CreateRecipeMap();

        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        return recipeMap[name];
    }

    public ProductConfig[] GetAllProducts()
    {
        CreateProductMap();

        return productsMap.Values.ToArray();
    }

    public int[] GetRandomPrices(float multiplyValueA, float multiplayValueB)
    {
        var allProducts = GetAllProducts();
        int[] prices = new int[allProducts.Length];

        for (int i = 0; i < allProducts.Length; i++)
        {
            float random = UnityEngine.Random.Range(multiplyValueA, multiplayValueB);
            prices[i] = (int)(allProducts[i].ProductCost * random);
        }

        return prices;
    }

    private void CreateRecipeMap()
    {
        if (recipeMap == null)
        {
            recipeMap = new();
            var allRecipes = Resources.LoadAll<RecipeConfig>("Recipes");
            Array.Sort(allRecipes, (x, y) => x.Price.CompareTo(y.Price));

            for (int i = 0; i < allRecipes.Length; i++)
            {
                if (!string.IsNullOrEmpty(allRecipes[i].Description))
                {
                    recipeMap[allRecipes[i].Name] = allRecipes[i];
                }
            }
        }
    }

    public RecipeConfig GetRandomSellingDrink()
    {
        CreateRecipeMap();

        var availableDrinks = recipeMap.Values.Where(item => knownRecipes.IsSelling(item.Name) && item.IsDrink).ToArray();
        
        if(availableDrinks.Length > 0)
        {
            int random = UnityEngine.Random.Range(0, availableDrinks.Length);
            return availableDrinks[random];
        }

        return null;
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
        CreateRecipeMap();

        var remainingRecipes = recipeMap.Values.ToList();

        foreach(var recipe in recipeMap.Values)
        {
            if (recipe.Products.Count != products.Count || recipe.CookingPlace != place)
            {
                remainingRecipes.Remove(recipe);
            }

            for (int i = 0; i < products.Count; i++)
            {
                if (!recipe.Products.Contains(products[i]))
                {
                    remainingRecipes.Remove(recipe);
                }
            }
        }

        if (remainingRecipes.Count == 1)
        {
            var recipe = remainingRecipes[0];

            if (!knownRecipes.IsAvailable(recipe.Name))
            {
                knownRecipes.AddRecipe(recipe.Name);
            }

            return recipe;
        }

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

    public List<RecipeConfig> GetRecipesByName(List<string> names)
    {
        var list = new List<RecipeConfig>();
        CreateRecipeMap();

        for (int i = 0; i < names.Count; i++)
        {
            list.Add(recipeMap[names[i]]);
        }

        return list;
    }

    public string GetRandomRecipeName()
    {
        CreateRecipeMap();

        var names = recipeMap.Keys.ToArray();

        for (int i = 0;i < names.Length;i++)
        {
            if (!knownRecipes.IsAvailable(names[i]))
            {
                return names[i];
            }
        }

        return null;
    }

}
