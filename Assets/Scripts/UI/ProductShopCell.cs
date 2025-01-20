using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductShopCell : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text price;

    [SerializeField] private Color[] colors;
    private Button button;
    public string ItemName { get; private set; }

    private void Awake() => button = GetComponent<Button>();

    public void Bind(ProductConfig config, int price, int priceIndex)
    {
        this.price.text = $"{price}$";
        image.sprite = config.ProductIcon;
        ItemName = config.ProductName;

        this.price.color = colors[priceIndex];
    }

    public void ChangeEvent(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
