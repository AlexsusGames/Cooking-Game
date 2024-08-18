using System;
using UnityEngine;

public interface IReadOnlyInventoryGrid : IReadOnlyInventory
{
    event Action<string, int> OnAddingItem;
    event Action<string, int> OnRemovingItem;
    Vector2Int Size { get; }
    IReadOnlyInventorySlot[,] GetSlots();
}
