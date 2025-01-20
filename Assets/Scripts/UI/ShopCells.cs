using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShopCells : MonoBehaviour
{
    [SerializeField] private Transform shopParent;
    [SerializeField] private GameObject productPrefab;
    private List<ProductShopCell> items = new();

    public void Init(int[] prices, int[] indexes, ProductConfig[] allProducts)
    {
        for (int i = 0; i < allProducts.Length; i++)
        {
            var cell = Instantiate(productPrefab, shopParent);
            cell.TryGetComponent(out ProductShopCell shopCell);
            shopCell.Bind(allProducts[i], prices[i], indexes[i]);
            items.Add(shopCell);
        }
    }

    public List<ProductShopCell> GetItems()
    {
        return items;
    }
}
