using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSource : InteractiveManager
{
    [SerializeField] private ProductConfig product;
    private string[] advices = { "Не хватает места" };

    public override void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var grid = inventory.Setup();

        if (grid.CanTake(product.ProductName))
        {
            grid.AddItems(product.ProductName);
        }
        else ShowAdvice(advices[0]);
    }

    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            CachedKeys = advices;
        }

        return CachedKeys;
    }

    public override void Set(params string[] param) => advices = param;
}
