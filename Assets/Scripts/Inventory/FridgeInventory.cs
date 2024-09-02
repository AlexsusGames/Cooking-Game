using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeInventory : InventoryManager, IInventoryData
{
    [SerializeField] private InventoryTypes type;
    [SerializeField] private InventoryGridView view;

    private Outline outline;
    private InventoryDataLoader dataLoader = new();
    private InventoryGridData data;

    private void Awake() => outline = GetComponent<Outline>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Inventory player))
        {
            outline.enabled = true;
            view.gameObject.SetActive(true);
            view.Setup(myInventory);

            BindButtons(view);

            anotherInventory = player.Setup(myInventory);
            Cursor.visible = true;
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
            Cursor.visible = false;
        }
    }
    public void Load()
    {
        data = dataLoader.GetInventory(type);
        myInventory = new InventoryGrid(data);
    }

    public void Save()
    {
        dataLoader.SaveInventory(data, type);
    }
}
