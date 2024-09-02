using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryData 
{
    private Dictionary<string, int> orderedProducts = new();

    public void CreateOrder(string name, int count)
    {
        orderedProducts[name] = count;
    }

    public Dictionary<string, int> GetOrderedProducts()
    {
        return orderedProducts;
    }
}
