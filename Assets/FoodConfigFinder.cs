using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FoodConfigFinder 
{
    private ProductConfig[] products;

    public ProductConfig GetProductByName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            if (products == null) products = Resources.LoadAll<ProductConfig>("Products");

            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].ProductName == name) return products[i];
            }
        }
        return null;
    }
}
