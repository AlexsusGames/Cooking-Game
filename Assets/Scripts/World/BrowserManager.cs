using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BrowserManager : MonoBehaviour
{
    [SerializeField] private ShopCells shopCells;
    [SerializeField] private Shopping—art shopping—art;

    [Inject] private InteractSound sound;
    private int amountToAdd = 1;

    private FoodConfigFinder foodConfigFinder = new();

    private void Start()
    {
        var prices = foodConfigFinder.GetRandomPrices();
        var configs = foodConfigFinder.GetAllProducts();
        shopCells.Init(prices, configs);
        
        var buttons = shopCells.GetItems();

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            UnityAction action = () =>
            {
                ProductToBuy info = new(buttons[index].ItemName, prices[index], amountToAdd);
                PlayClick();
                shopping—art.AddProductToCart(info);
            };

            buttons[i].ChangeEvent(action);
        }
    }

    private void OnDisable() => gameObject.SetActive(false);

    public void SetCountToAdd(int amount)
    {
        amountToAdd = amount;
    }

    public void PlayClick() => sound.Play(NonLoopSounds.Click);
}
