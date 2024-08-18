using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] protected int countItems;
    protected InventoryGrid myInventory;
    protected InventoryGrid anotherInventory;

    protected void BindButtons(InventoryGridView view)
    {
        var buttons = view.GetInventoryButtons();
        for (int i = 0; i < buttons.Count; i++)
        {
            var position = buttons[i].GetComponent<InventorySlotView>().Position;
            buttons[i].onClick.AddListener(() => PutItems(position, countItems));
        }
    }
    protected void PutItems(Vector2Int coords, int count)
    {
        if (anotherInventory != null)
        {
            var itemToRemove = myInventory.GetSlots()[coords.x, coords.y];
            var itemId = itemToRemove.ItemId;

            if (!itemToRemove.isEmpty && anotherInventory.CanTake(itemId, count))
            {
                var result = myInventory.RemoveItem(coords, count);
                var amount = result.RemovedItemsAmount;

                anotherInventory.AddItems(itemId, amount);
            }
        }
    }
}
