using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeviceShopView : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Image icon;
    public DeviceConfig config { get; private set; }

    public void SetData(DeviceConfig config, bool isBought)
    {
        this.config = config;

        icon.sprite = config.Icon;
        nameText.text = config.Name;
        priceText.text = $"{config.Price}$";

        buyButton.interactable = !isBought;
        buttonText.text = isBought ? "�������" : "������";
    }

    public Button GetButton() => buyButton;
}
