using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class InventoryGrid : IReadOnlyInventoryGrid
{
    private int slotCapacity = 10;
    public Vector2Int Size => data.Size;

    public string OwnerId => data.InventoryId;

    private readonly InventoryGridData data;
    private readonly Dictionary<Vector2Int, InventorySlot> slotsMap = new();

    public event Action<string, int> OnAddingItem;
    public event Action<string, int> OnRemovingItem;

    public InventoryGrid(InventoryGridData data)
    {
        this.data = data;

        var size = data.Size;
        for(int i = 0; i < size.x; i++)
        {
            for(int j = 0; j < size.y; j++)
            {
                var index = i * size.y + j;
                var slotData = data.Slots[index];
                var slot = new InventorySlot(slotData);
                var position = new Vector2Int(i, j);

                slotsMap[position] = slot;
            }
        }
    }
    public List<string> GetInventoryItems()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                var pos = new Vector2Int(i, j);
                var slot = slotsMap[pos];

                if (!slot.isEmpty || slot.Amount != 0)
                {
                    items.Add(slot.ItemId);
                }
            }
        }
        return items;
    }

    public bool CanTake(string itemId, int amount)
    {
        var itemsToAdd = amount;

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                var pos = new Vector2Int(i, j);
                var slot = slotsMap[pos];

                if (slot.Amount == slotCapacity)
                {
                    continue;
                }

                if(slot.ItemId != itemId &&  slot.Amount != 0)
                {
                    continue;
                }

                var canTake = slotCapacity - slot.Amount;
                itemsToAdd -= canTake;

                if(itemsToAdd <= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public AddItemsToInventoryGridResult AddItems(string itemId, int amount = 1)
    {
        var remainingAmount = amount;
        var itemsAddedToSlotsWithSameItems = AddToSlotWithSameItems(itemId, remainingAmount, out remainingAmount);

        if(remainingAmount == 0)
        {
            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedToSlotsWithSameItems);
        }

        var itemsAddedToAvailableSlot = AddToFirstAvailableSlot(itemId, remainingAmount, out remainingAmount);
        var totalAddedItems = itemsAddedToAvailableSlot + itemsAddedToSlotsWithSameItems;

        OnAddingItem?.Invoke(itemId, totalAddedItems);
        return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedItems);
    }
    public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, string itemId, int amount = 1)
    {
        var slot = slotsMap[slotCoords];
        var newValue = slot.Amount + amount;
        var itemsAddedAmount = 0;

        if (slot.isEmpty)
        {
            slot.ItemId = itemId;
        }

        if(newValue > slotCapacity)
        {
            var remainingItems = newValue - slotCapacity;
            var itemsToAddAmount = slotCapacity - slot.Amount;
            itemsAddedAmount += itemsToAddAmount;
            slot.Amount = slotCapacity;

            var result = AddItems(itemId, remainingItems);
            itemsAddedAmount += result.AddedItemsAmount;
        }
        else
        {
            itemsAddedAmount = amount;
            slot.Amount = newValue;
        }

        return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
    }
    public RemoveItemsFromInventoryResult RemoveItem(Vector2Int slotCoords, int amount = 1)
    {
        var slot = slotsMap[slotCoords];

        if(slot.isEmpty)
        {
            return new RemoveItemsFromInventoryResult(OwnerId, 0, 0);
        }

        if(slot.Amount < amount)
        {
            amount = slot.Amount;
        }

        slot.Amount -= amount;

        OnRemovingItem?.Invoke(slot.ItemId, amount);
        return new RemoveItemsFromInventoryResult(OwnerId, amount, amount);
    }
    public (RemoveItemsFromInventoryResult result, string item) RemoveFirstItem()
    {
        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                var slot = slotsMap[new Vector2Int(i, j)];

                if (slot.isEmpty)
                {
                    continue;
                }

                var itemToRemove = slot.Amount;
                slot.Amount = 0;

                return (new RemoveItemsFromInventoryResult(OwnerId, itemToRemove, itemToRemove), slot.ItemId);
            }
        }
        return (new RemoveItemsFromInventoryResult(OwnerId, 0, 0), null);
    }

    public IReadOnlyInventorySlot[,] GetSlots()
    {
        var array = new IReadOnlyInventorySlot[Size.x, Size.y];

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                var position = new Vector2Int(i, j);

                array[i,j] = slotsMap[position];
            }
        }

        return array;
    }

    public bool Has(string itemId, int amount)
    {
        var itemAmount = 0;

        var slots = data.Slots;

        foreach (var slot in slots)
        {
            if(slot.ItemId == itemId)
            {
                itemAmount += slot.Amount;
            }
        }

        return itemAmount >= amount;
    }

    private int AddToSlotWithSameItems(string itemId, int amount, out int remainingAmount)
    {
        var itemsAddedAmount = 0;
        remainingAmount = amount;

        for (int i = 0;i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                var coords = new Vector2Int(i, j);
                var slot = slotsMap[coords];

                if (slot.isEmpty || slot.Amount == slotCapacity || slot.ItemId != itemId)
                {
                    continue;
                }

                var newValue = slot.Amount + remainingAmount;

                if(newValue > slotCapacity)
                {
                    remainingAmount = newValue - slotCapacity;
                    var itemsToAdd = slotCapacity - slot.Amount;
                    itemsAddedAmount += itemsToAdd;
                    slot.Amount = slotCapacity;

                    if(remainingAmount == 0)
                    {
                        return itemsAddedAmount;
                    }
                }
                else
                {
                    itemsAddedAmount += remainingAmount;
                    slot.Amount = newValue;
                    remainingAmount = 0;

                    return itemsAddedAmount;
                }
            }
        }
        return itemsAddedAmount;
    }

    private int AddToFirstAvailableSlot(string itemId, int amount, out int remainingAmount)
    {
        var itemsAddedAmount = 0;
        remainingAmount = amount;

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                var coords = new Vector2Int(i, j);
                var slot = slotsMap[coords];

                if (slot.isEmpty)
                {
                    var newValue = remainingAmount;
                    slot.ItemId = itemId;

                    if (newValue > slotCapacity)
                    {
                        remainingAmount = newValue - slotCapacity;
                        var itemsToAdd = slotCapacity;
                        itemsAddedAmount += itemsToAdd;
                        slot.Amount = slotCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
        }
        return itemsAddedAmount;
    }
}
