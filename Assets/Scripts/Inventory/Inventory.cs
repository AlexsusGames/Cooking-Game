using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : InventoryManager, IInventoryData
{
    private const InventoryTypes TYPE = InventoryTypes.Player;
    [SerializeField] private InventoryGridView view;
    private InventoryDataLoader loader = new();
    private InventoryGridData data;

    public InventoryGrid Setup(InventoryGrid inventory = null)
    {
        anotherInventory = inventory;
        return myInventory;
    }

    public List<string> GetProducts()
    {
        return myInventory.GetInventoryItems();
    }

    public void RemoveProducts()
    {
        for (int i = 0; i < myInventory.Size.x; i++)
        {
            for(int j = 0; j < myInventory.Size.y; j++)
            {
                myInventory.RemoveItem(new Vector2Int(i, j));
            }
        }
    }

    public void Load()
    {
        data = loader.GetInventory(TYPE);
        myInventory = new(data);
        view.Setup(myInventory);

        BindButtons(view);
    }

    public void Save()
    {
        loader.SaveInventory(data, TYPE);
    }
}
