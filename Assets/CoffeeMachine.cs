using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CoffeeMachine : InteractiveManager
{
    [SerializeField] private ParticleGroup effect;
    [SerializeField] private ProductConfig[] coffeeRecipe;
    [SerializeField] private CoffeeKeeper coffeeKeeper;
    [SerializeField] private GameObject spoiltCoffee;
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
                if (!coffeeKeeper.IsMaxCountOfObjects)
                {
                    isWorking = true;
                    TaxCounter.Taxes += 0.5f;

                    effect.Play();

                    var model = foodConfigFinder.CookingFood(inventoryProducts, InteractivePlaces.CoffeeMachine);

                    inventory.RemoveProducts();

                    await Task.Delay(5000);

                    if (model != null)
                    {
                        coffeeKeeper.CreateObject(model.Model);
                    }
                    else coffeeKeeper.CreateObject(spoiltCoffee);

                    effect.Stop();
                    isWorking = false;
                }
                else ShowAdvice("Не хватает места на столе.");
            }
            else ShowAdvice("Чего-то не хватает...");
        }
        else ShowAdvice("Рука занята.");
    }

    private bool CompareRecipes(List<ProductConfig> recipe)
    {
        if (recipe.Count < coffeeRecipe.Length)
            return false;

        for (int i = 0; i < coffeeRecipe.Length; i++)
        {
            if (!recipe.Contains(coffeeRecipe[i]))
                return false;
        }

        return true;
    }
}
