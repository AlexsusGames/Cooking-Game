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
    private Button button;
    public string ItemName { get; private set; }

    private void Awake() => button = GetComponent<Button>();

    public void Bind(ProductConfig config, int price)
    {
        this.price.text = $"{price}$";
        image.sprite = config.ProductIcon;
        ItemName = config.ProductName;
    }

    public void ChangeEvent(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
