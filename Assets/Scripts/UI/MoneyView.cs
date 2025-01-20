using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text profitText;
    [SerializeField] private Bank bank;
    [SerializeField] private Color green;
    [SerializeField] private Color red;
    private int cachedAmount;

    private void Start()
    {
        moneyText.text = $"{bank.Get()}$";
        bank.MoneyChanged += UpdateView;
        cachedAmount = bank.Get();
    }

    public void UpdateView(int newAmount)
    {
        moneyText.text = $"{newAmount}$";

        var profit = newAmount - cachedAmount;
        ShowProfit(profit);

        cachedAmount = newAmount;
    }

    public async void ShowProfit(int profit)
    {
        profitText.color = profit > 0 ? green : red;
        profitText.text = profit > 0 ? $"+{profit}$" : $"{profit}$";

        await Task.Delay(2000);
        profitText.text = "";
    }
}
