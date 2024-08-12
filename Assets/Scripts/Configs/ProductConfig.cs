using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateConfig/Product", fileName = "Product")]
public class ProductConfig : ScriptableObject
{
    public string ProductName;
    public int ProductCost;
    public Sprite ProductIcon;
}
