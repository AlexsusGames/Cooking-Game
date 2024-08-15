using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventoryGridView view;
    [SerializeField] private InventoryTypes inventoryType;
    private InventoryDataLoader loader = new();
    private InventoryGrid inventory;

    private void Awake()
    {
        var inventoryData = loader.GetInventory(inventoryType);
        inventory = new(inventoryData);
        view.Setup(inventory);
        Invoke(nameof(test), 2);
    }

    private void test()
    {
        inventory.AddItems("Клубника", 4);
        inventory.AddItems("Молоко", 7);
        inventory.AddItems("Курица", 13);
    }

}
