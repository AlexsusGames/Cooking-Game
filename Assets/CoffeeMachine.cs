using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeMachine : InteractiveManager
{
    [SerializeField] private ParticleGroup effect;
    [SerializeField] private RecipeConfig coffeeRecipe;
    [SerializeField] private CoffeeKeeper coffeeKeeper;
    private FoodConfigFinder foodConfigFinder = new();
    private bool isWorking;

    public override async void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var inventoryProducts = foodConfigFinder.GetProductsByName(inventory.GetProducts());
        var handler = player.GetComponent<ObjectHandler>();
        var handleObject = handler.GetObject();

        if (handleObject == null && !isWorking)
        {
            if (CompareRecipes(inventoryProducts))
            {
                isWorking = true;
                TaxCounter.Taxes += 0.5f;

                effect.Play();

                var model = foodConfigFinder.CookingFood(inventoryProducts, InteractivePlaces.CoffeeMachine);

                inventory.RemoveProducts();

                await Task.Delay(5000);

                if (model != null)
                {
                    coffeeKeeper.CreateCoffee();
                }

                effect.Stop();
                isWorking = false;
            }
            else ShowAdvice("Чего-то не хватает...");
        }
        else ShowAdvice("Рука занята.");
    }

    private bool CompareRecipes(List<ProductConfig> recipe)
    {
        if (recipe.Count != coffeeRecipe.Products.Count)
            return false;

        for (int i = 0; i < recipe.Count; i++)
        {
            if (!coffeeRecipe.Products.Contains(recipe[i]))
                return false;
        }

        return true;
    }
}
