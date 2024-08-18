using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text amountText;
    public Vector2Int Position;

    private FoodConfigFinder foodConfigFinder = new();

    public void Setup(IReadOnlyInventorySlot slot)
    {
        UpdateItem(slot.ItemId);
        UpdateAmount(slot.Amount);
    }

    public void UpdateItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            itemImage.enabled = false;
        }
        else
        {
            itemImage.enabled = true;
            itemImage.sprite = foodConfigFinder.GetProductByName(itemName).ProductIcon;
        }
    }
    public void UpdateAmount(int amount)
    {
        if (amount > 0) amountText.text = $"x{amount}";
        else amountText.text = string.Empty;
    }
}
