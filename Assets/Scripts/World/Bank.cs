using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private int money;
    private int dayLoses;

    private readonly Wallet wallet = new();

    public event Action<int> MoneyChanged;

    public void Init()
    {
        dayLoses = 0;
        money = wallet.GetMoney();
    }

    public void SaveMoney() => wallet.SaveMoney(money);

    public bool Has(int money) => this.money >= money;

    public void Change(int sum)
    {
        money += sum;
        MoneyChanged?.Invoke(money);

        if(sum < 0)
        {
            dayLoses -= sum;
        }
    }
    public int GetLoses() => dayLoses;
    public int Get() => money;
}
