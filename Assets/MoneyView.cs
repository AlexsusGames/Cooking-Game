using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Bank bank;

    private void Start()
    {
        moneyText.text = $"{bank.Get()}$";
        bank.MoneyChanged += UpdateView;
    }

    public void UpdateView(int newAmount)
    {
        moneyText.text = $"{newAmount}$";
    }
}
