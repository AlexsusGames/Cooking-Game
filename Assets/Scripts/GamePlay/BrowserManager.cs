using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BrowserManager : MonoBehaviour
{
    [SerializeField] private ShopCells shopCells;
    [SerializeField] private Shopping—art shopping—art;

    private const string FIRST_DAY_KEY = "First_day_price";

    [Inject] private InteractSound sound;
    private int amountToAdd = 1;

    private FoodConfigFinder foodConfigFinder = new();

    private void Start()
    {
        float secondMultiplier = PlayerPrefs.HasKey(FIRST_DAY_KEY) ? 1.2f : 1.0f;
        PlayerPrefs.SetString(FIRST_DAY_KEY, "");

        var prices = foodConfigFinder.GetRandomPrices(0.7f, secondMultiplier);
        var configs = foodConfigFinder.GetAllProducts();
        shopCells.Init(prices.prices,prices.colorIndex, configs);
        
        var buttons = shopCells.GetItems();

        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            UnityAction action = () =>
            {
                ProductToBuy info = new(buttons[index].ItemName, prices.prices[index], amountToAdd);
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
