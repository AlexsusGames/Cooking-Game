using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ShoppingСart : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject productPrefab;
    [SerializeField] private Bank bank;
    [SerializeField] private Delivery delivery;
    [SerializeField] private InternerConfirmMenu confirmWindow;

    private const string QUEST_REQUEST = "tutor2";
    [Inject] private QuestHandler questHander;
    [Inject] private InteractSound sound;

    private List<ProductView> items = new();
    private DeliveryData deliveryData;

    private void OnDisable() => Clear();
    private void OnEnable() => SetInputBlock(delivery.IsDelivering);
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
            sound.Play(NonLoopSounds.Click);
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

    public void SetInputBlock(bool value)
    {
        var blockObj = confirmWindow.transform.parent;
        blockObj.gameObject.SetActive(value);
    }

    public void Order()
    {
        deliveryData = new DeliveryData();
        var price = GetSumOfOrder() + 20;

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

            questHander.TryChangeProgress(QUEST_REQUEST); // tutor
        }
        else
        {
            confirmWindow.ShowError("Не хватает денег");
        }
    }
}
