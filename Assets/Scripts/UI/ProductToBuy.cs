using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProductToBuy 
{
    public string ProductName;
    public int Price;
    public int Amount;

    public int TotalCost => Price * Amount;

    public ProductToBuy(string name, int price, int amount)
    {
        ProductName = name;
        Price = price;
        Amount = amount;
    }

    public void ChangeAmount(int amount)
    {
        Amount += amount;
    }
}
