using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingСart : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject productPrefab;
    private List<ProductView> items = new();

    private void OnDisable() => Clear();

    public void AddProductToCart(ProductToBuy productToBuy)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].ProductToBuy.ProductName == productToBuy.ProductName)
            {
                items[i].ChangeAmount(productToBuy.Amount);
                return;
            }
        }
        CreateNewPosition(productToBuy);
        
    }
    private void Clear()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i].gameObject);
        }

        items.Clear();
    }
    private void CreateNewPosition(ProductToBuy info)
    {
        var product = Instantiate(productPrefab, parent);
        product.TryGetComponent(out ProductView view);
        view.Bind(info);
        items.Add(view);

        UnityAction action = () =>
        {
            items.Remove(view);
            Destroy(product);
        };

        view.ChangeEvent(action);
    }
}
