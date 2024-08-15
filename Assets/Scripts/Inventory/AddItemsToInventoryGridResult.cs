using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public readonly struct AddItemsToInventoryGridResult 
{
    public readonly string InventoryId;
    public readonly int ItemsToAddAmount;
    public readonly int AddedItemsAmount;

    public int ItemsNotAdded => ItemsToAddAmount - AddedItemsAmount;

    public AddItemsToInventoryGridResult(string inventoryId, int itemsToAdd, int addedItems)
    {
        InventoryId = inventoryId;
        ItemsToAddAmount = itemsToAdd;
        AddedItemsAmount = addedItems;
    }
}
