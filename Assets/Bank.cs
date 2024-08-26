using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    private Wallet wallet = new();
    private int money;

    public event Action<int> MoneyChanged;

    private void Awake()
    {
        money = wallet.GetMoney();
        money += 10000;
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
