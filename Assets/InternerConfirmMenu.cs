using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InternerConfirmMenu : MonoBehaviour
{
    [SerializeField] private GameObject blockObject;
    [SerializeField] private TMP_Text dealText;
    [SerializeField] private TMP_Text errorText;

    public void SetDealText(int money)
    {
        gameObject.SetActive(true);
        blockObject.SetActive(true);
        dealText.text = $"Оплатить суму заказа в размере: <color=green>{money}$?";
        errorText.text = "";
    }

    public void ShowError(string message)
    {
        errorText.text = message;
    }
}
