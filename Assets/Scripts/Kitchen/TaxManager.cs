using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaxManager : MonoBehaviour
{
    [SerializeField] private Bank bank;
    [SerializeField] private TMP_Text taxText;
    private const string Key = "Tax_Amount_Save";
    private int taxAmount;

    private void OnEnable() => UpdateView();

    public void Load() => taxAmount = PlayerPrefs.GetInt(Key);
    public void Save() => PlayerPrefs.SetInt(Key, taxAmount);
    public void Change(int amount) => taxAmount += amount;

    public void Pay()
    {
        if(bank.Has(taxAmount))
        {
            bank.Change(-taxAmount);
            Change(-taxAmount);
            UpdateView();
        }
    }

    private void UpdateView()
    {
        taxText.text = $"- {taxAmount}$/100$";
    }
}
