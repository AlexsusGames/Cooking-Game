using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridView : MonoBehaviour
{
    [SerializeField] private List<InventorySlotView> slotView = new();

    public void Setup(IReadOnlyInventoryGrid inventory)
    {
        var slots = inventory.GetSlots();
        int index = 0;

        foreach (var slot in slots)
        {
            slot.ItemIdChanged += slotView[index].UpdateItem;
            slot.ItemAmountChanged += slotView[index].UpdateAmount;
            slotView[index].Setup(slot);
            index++;
        }
    }

}
