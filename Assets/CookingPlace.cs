using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CookingPlace : InteractiveManager
{
    [SerializeField] private InteractivePlaces place;
    [SerializeField] private ParticleGroup effect;

    private FoodConfigFinder foodConfigFinder = new();

    private void Awake()
    {
        effect.Stop();
    }

    public override async void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var inventoryProducts = foodConfigFinder.GetProductsByName(inventory.GetProducts());

        if (player.GetComponent<ObjectHandler>().GetObject().TryGetComponent(out Dish dish))
        {
            if(dish.GetFood() == null && inventoryProducts.Count > 1)
            {
                effect.Play();
                player.Interact();

                var model = foodConfigFinder.CookingFood(inventoryProducts, place);

                inventory.RemoveProducts();

                if(model != null)
                {
                    dish.SetFood(model);
                }

                await Task.Delay(5000);
            }
        }

        FinishInterating(player);
    }

    private void FinishInterating(MoveController player)
    {
        player.FinishInteracting();
        effect.Stop();
    }
}
public enum InteractivePlaces
{
    SlicingTable,
    Stove,
    WashStand
}