using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeviceShopView : MonoBehaviour
{
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Image icon;
    [SerializeField] private Image buttonIcon;
    [SerializeField] private Sprite checkMarkSprite;
    public DeviceConfig config { get; private set; }

    public void SetData(DeviceConfig config, bool isBought)
    {
        this.config = config;

        icon.sprite = config.Icon;
        nameText.text = config.Name;
        priceText.text = $"{config.Price}$";

        buyButton.interactable = !isBought;
        buyButton.image.color = isBought ? Color.yellow : buyButton.image.color;
        if (isBought) buttonIcon.sprite = checkMarkSprite;
    }

    public Button GetButton() => buyButton;
}
