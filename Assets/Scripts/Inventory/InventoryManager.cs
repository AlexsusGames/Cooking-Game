using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] protected bool giveAllItems = false;
    [Inject] protected InteractSound sound;
    protected InventoryGrid myInventory;
    protected InventoryGrid anotherInventory;

    protected void BindButtons(InventoryGridView view)
    {
        var buttons = view.GetInventoryButtons();
        for (int i = 0; i < buttons.Count; i++)
        {
            var position = buttons[i].GetComponent<InventorySlotView>().Position;
            buttons[i].onClick.AddListener(() => PutItems(position, giveAllItems));
        }
    }
    protected void PutItems(Vector2Int coords, bool all)
    {
        if (anotherInventory != null)
        {
            var itemToRemove = myInventory.GetSlots()[coords.x, coords.y];
            var itemId = itemToRemove.ItemId;

            if (!itemToRemove.isEmpty)
            {
                int count = all ? itemToRemove.Amount : 1;
                var result = anotherInventory.AddItems(itemId, count);

                myInventory.RemoveItem(coords, result.AddedItemsAmount);

                sound.Play(NonLoopSounds.Click);
            }
        }
    }
}
