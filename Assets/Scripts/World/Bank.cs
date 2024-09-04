using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [HideInInspector] public float Taxes;
    [HideInInspector] public float IncomeTaxes;
    private int money;

    private Wallet wallet = new();

    public event Action<int> MoneyChanged;

    public static Bank Instance;

    private void Awake()
    {
        money = wallet.GetMoney();
        Instance = this;
        Taxes = 0;
        IncomeTaxes = 0;
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
