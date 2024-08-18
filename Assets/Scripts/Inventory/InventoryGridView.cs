using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridView : MonoBehaviour
{
    [SerializeField] private List<InventorySlotView> slotView = new();
    private List<Vector2Int> positions;

    public void Setup(IReadOnlyInventoryGrid inventory)
    {
        CreateGrid(inventory.Size);
        var slots = inventory.GetSlots();
        int index = 0;

        foreach (var slot in slots)
        {
            slotView[index].Position = positions[index];
            slot.ItemIdChanged += slotView[index].UpdateItem;
            slot.ItemAmountChanged += slotView[index].UpdateAmount;
            slotView[index].Setup(slot);
            index++;
        }
    }

    private void CreateGrid(Vector2Int size)
    {
        positions = new List<Vector2Int>();

        for (int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                positions.Add(new Vector2Int(i, j));
            }
        }
    }

    public List<Button> GetInventoryButtons()
    {
        List<Button> buttons = new List<Button>();

        for (int i = 0; i < slotView.Count; i++)
        {
            slotView[i].TryGetComponent(out Button button);
            button.onClick.RemoveAllListeners();
            buttons.Add(button);
        }

        return buttons;
    }

}
