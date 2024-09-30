using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CookingPlace : InteractiveManager, IUpgradable
{
    [SerializeField] private InteractivePlaces place;
    [SerializeField] private ParticleGroup effect;
    [SerializeField] private RecipeConfig spoiltFood;

    private FoodConfigFinder foodConfigFinder = new();
    public InteractivePlaces InteractiveType => place;
    private int msTime = 5000;
    public int InteractiveTime { get => msTime; set => msTime = 5000 - value * 1000; }

    public override async void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var inventoryProducts = foodConfigFinder.GetProductsByName(inventory.GetProducts());
        var objHandler = player.GetComponent<ObjectHandler>();
        var obj = objHandler.GetObject();

        if (obj != null)
        {
            if(obj.TryGetComponent(out Dish dish))
            {
                if (dish.GetFood() == null && inventoryProducts.Count > 1)
                {
                    TaxCounter.Taxes += 0.5f;

                    effect.Play();
                    player.Interact();

                    var model = foodConfigFinder.CookingFood(inventoryProducts, place);

                    inventory.RemoveProducts();

                    await Task.Delay(msTime);

                    if (model != null)
                    {
                        dish.SetFood(model);
                    }
                    else dish.SetFood(spoiltFood);

                    objHandler.UpdateFoodImage();

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
    WashStand = 3,
    CoffeeMachine = 2
}