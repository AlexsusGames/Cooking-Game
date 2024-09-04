using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CookingPlace : InteractiveManager
{
    [SerializeField] private InteractivePlaces place;
    [SerializeField] private ParticleGroup effect;
    [SerializeField] private RecipeConfig spoiltFood;

    private FoodConfigFinder foodConfigFinder = new();

    public override async void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var inventoryProducts = foodConfigFinder.GetProductsByName(inventory.GetProducts());
        var handleObject = player.GetComponent<ObjectHandler>().GetObject();

        if (handleObject != null)
        {
            if(handleObject.TryGetComponent(out Dish dish))
            {
                if (dish.GetFood() == null && inventoryProducts.Count > 1)
                {
                    Bank.Instance.Taxes += 0.5f;

                    effect.Play();
                    player.Interact();

                    var model = foodConfigFinder.CookingFood(inventoryProducts, place);

                    inventory.RemoveProducts();

                    if (model != null)
                    {
                        dish.SetFood(model);
                    }
                    else dish.SetFood(spoiltFood);

                    await Task.Delay(5000);

                    player.FinishInteracting();
                    effect.Stop();
                }
            }
            else ShowAdvice("Тарелку забыл.");
        }
    }
}
public enum InteractivePlaces
{
    SlicingTable = 0,
    Stove = 1,
    WashStand,
    CoffeeMachine
}