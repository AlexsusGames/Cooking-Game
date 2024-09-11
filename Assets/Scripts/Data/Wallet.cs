using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    private const string Key = "Money_Save";

    public int GetMoney()
    {
        if (!PlayerPrefs.HasKey(Key))
        {
            PlayerPrefs.SetInt(Key, 1000);
        }


        int amount = PlayerPrefs.GetInt(Key);
        return amount;
    }

    public void SaveMoney(int amount)
    {
        PlayerPrefs.SetInt(Key, amount);
        PlayerPrefs.Save();
    }
}
