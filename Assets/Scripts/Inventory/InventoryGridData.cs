using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryGridData 
{
    public string InventoryId;
    public List<InventorySlotData> Slots;
    public Vector2Int Size;
}