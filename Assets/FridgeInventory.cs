using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInventory : InventoryManager
{
    [SerializeField] private InventoryTypes type;
    [SerializeField] private InventoryGridView view;

    private FoodConfigFinder foodConfigFinder = new();
    private Outline outline;
    private InventoryDataLoader data = new();

    private void Awake()
    {
        outline = GetComponent<Outline>();
        var invData = data.GetInventory(type);
        myInventory = new InventoryGrid(invData);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Inventory player))
        {
            outline.enabled = true;
            view.gameObject.SetActive(true);
            view.Setup(myInventory);

            BindButtons(view);

            anotherInventory = player.Setup(myInventory);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Inventory player))
        {
            outline.enabled = false;
            view.gameObject.SetActive(false);

            player.Setup(null);
            anotherInventory = null;
        }
    }
}
