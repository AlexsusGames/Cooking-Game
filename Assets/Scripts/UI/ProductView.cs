using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ProductView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text price;
    [SerializeField] private TMP_Text count;
    [SerializeField] private TMP_Text totalPrice;
    public ProductToBuy ProductToBuy { get; private set; }

    private FoodConfigFinder foodConfigFinder = new();
    private Button button;
    public void Bind(ProductToBuy product)
    {
        button = GetComponent<Button>();
        image.sprite = foodConfigFinder.GetProductByName(product.ProductName).ProductIcon;
        ProductToBuy = product;

        UpdateView(product);
    }

    public void ChangeAmount(int newAmount)
    {
        var temp = ProductToBuy;
        temp.ChangeAmount(newAmount);
        ProductToBuy = temp;
        UpdateView(ProductToBuy);
    }

    public void UpdateView(ProductToBuy product)
    {
        price.text = $"{product.Price}$";
        count.text = product.Amount.ToString();
        totalPrice.text = $"{product.TotalCost}$";
    }
    public void ChangeEvent(UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}
