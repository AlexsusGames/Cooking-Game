using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSource : InteractiveManager
{
    [SerializeField] private ProductConfig product;

    public override void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var grid = inventory.Setup();

        if (grid.CanTake(product.ProductName))
        {
            grid.AddItems(product.ProductName);
        }
        else ShowAdvice("Не хватает места");
    }
}
