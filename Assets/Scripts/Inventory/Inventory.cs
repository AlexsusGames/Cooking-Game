using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : InventoryManager
{
    [SerializeField] private InventoryGridView view;
    [SerializeField] private InventoryTypes inventoryType;
    private InventoryDataLoader loader = new();

    private void Awake()
    {
        var inventoryData = loader.GetInventory(inventoryType);
        myInventory = new(inventoryData);
        view.Setup(myInventory);

        BindButtons(view);

        myInventory.AddItems("Хлеб");
        myInventory.AddItems("Бекон");
        myInventory.AddItems("Сыр");
        myInventory.AddItems("Салат");
    }

    public InventoryGrid Setup(InventoryGrid inventory)
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
}
