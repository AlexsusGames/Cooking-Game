using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class TaxManager : MonoBehaviour
{
    [SerializeField] private Bank bank;
    [SerializeField] private TMP_Text taxText;
    [Inject] private SteamAchievements achievements;
    private const string Key = "Tax_Amount_Save";
    private int taxAmount;
    public bool IsTaxDebt => taxAmount >= 100;

    private void OnEnable() => UpdateView();
    public void Load() => taxAmount = PlayerPrefs.GetInt(Key);
    public void Save() => PlayerPrefs.SetInt(Key, taxAmount);
    public void Change(int amount) => taxAmount += amount;

    public void Pay()
    {
        if (bank.Has(taxAmount))
        {
            bank.Change(-taxAmount);
            Change(-taxAmount);

            achievements.TrySetAchievement(achievements.ACH_TAX);
        }
        else
        {
            var remainingMoney = bank.Get();
            Change(-remainingMoney); 
            bank.Change(-remainingMoney);
        }
        UpdateView();
    }

    private void UpdateView()
    {
        taxText.text = $"- {taxAmount}$/100$";
    }
}
