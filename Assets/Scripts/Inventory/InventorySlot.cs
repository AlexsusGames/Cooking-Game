using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : IReadOnlyInventorySlot
{
    public string ItemId
    {
        get => data.ItemId;
        set
        {
            if(data.ItemId != value)
            {
                data.ItemId = value;
                ItemIdChanged?.Invoke(value);
            }
        }
    }

    public int Amount
    {
        get => data.Amount;
        set
        {
            if (data.Amount != value)
            {
                data.Amount = value;
                ItemAmountChanged?.Invoke(value);
            }
        }
    }

    public bool isEmpty => Amount == 0 && string.IsNullOrEmpty(ItemId);

    public event Action<string> ItemIdChanged;
    public event Action<int> ItemAmountChanged;

    private readonly InventorySlotData data;

    public InventorySlot(InventorySlotData data)
    {
        this.data = data;
    }
}
