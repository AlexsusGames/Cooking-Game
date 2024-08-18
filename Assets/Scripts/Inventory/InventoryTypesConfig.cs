using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTypesConfig 
{
    public Vector2Int GetInventorySizes(InventoryTypes type)
    {
        if (type == InventoryTypes.CarTrunk) return new Vector2Int(10, 10);
        if (type == InventoryTypes.Player) return new Vector2Int(1, 6);
        else return new Vector2Int(4, 5);
    }
}
public enum InventoryTypes
{
    FridgeA,
    FridgeB,
    CarTrunk,
    Player
}
