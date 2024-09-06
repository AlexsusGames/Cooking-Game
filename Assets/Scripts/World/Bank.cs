using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private int money;

    private readonly Wallet wallet = new();

    public event Action<int> MoneyChanged;

    private void Awake()
    {
        money = wallet.GetMoney();
        TaxCounter.Reset();
    }

    public void SaveMoney()
    {
        wallet.SaveMoney(money);
    }

    public bool Has(int money)
    {
        return this.money >= money;
    }

    public void Change(int sum)
    {
        money += sum;
        MoneyChanged?.Invoke(money);
    }

    public int Get()
    {
        return money;
    }
}
