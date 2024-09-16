using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShopView : MonoBehaviour
{
    [SerializeField] private Image[] buyMarks;
    [SerializeField] private TMP_Text price;
    [SerializeField] private Button buyButton;
    public InteractivePlaces Type;

    public void UpdateView(int level, int price)
    {
        for (int i = 0; i < level; i++)
        {
            buyMarks[i].enabled = true;
        }

        this.price.text = $"{price}$";

        if (level == 3)
        {
            this.price.text = "";
            buyButton.gameObject.SetActive(false);
        }
    }
    public Button GetButton() => buyButton;
}
