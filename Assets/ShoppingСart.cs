using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class ShoppingСart : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Bank bank;
    [SerializeField] private Delivery delivery;
    [SerializeField] private InternerConfirmMenu confirmWindow;
    private List<ProductView> items = new();
    private DeliveryData deliveryData;

    private void Awake() => delivery.OnDelivered += CloseInputBlock;
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

    public int GetSumOfOrder()
    {
        int price = 0;

        for (int i = 0; i < items.Count; i++)
        {
            var product = items[i].ProductToBuy;
            price += product.TotalCost;
        }

        return price;
    }

    public void OpenConfirmWindow()
    {
        var price = GetSumOfOrder();

        if(price > 0)
        {
            confirmWindow.SetDealText(price);
        }
    }

    public void CloseInputBlock()
    {
        var blockObj = confirmWindow.transform.parent;
        blockObj.gameObject.SetActive(false);
    }

    public void Order()
    {
        deliveryData = new DeliveryData();
        var price = GetSumOfOrder();

        for (int i = 0; i < items.Count; i++)
        {
            var product = items[i].ProductToBuy;
            deliveryData.CreateOrder(product.ProductName, product.Amount);
        }

        if(bank.Has(price))
        {
            delivery.Deliver(deliveryData);
            bank.Change(-price);
            confirmWindow.gameObject.SetActive(false);
        }
        else
        {
            confirmWindow.ShowError("Не хватает денег");
        }
    }
}
