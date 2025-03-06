using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CurrentPartershipView : MonoBehaviour
{
    [SerializeField] private Sprite[] logos;
    [SerializeField] private Sprite[] fridgeIcons;
    [SerializeField] private Image fridgeIcon;
    [SerializeField] private Image logoIcon;
    [SerializeField] private TMP_Text debtText;
    [SerializeField] private int maxDebt;
    [SerializeField] private Bank bank;

    [Inject] private RatingManager ratingManager;

    public void SetData(DrinkType type)
    {
        gameObject.SetActive(true);

        int index = (int)type;

        fridgeIcon.sprite = fridgeIcons[index];
        logoIcon.sprite = logos[index];

        UpdateDebt();
    }

    private void UpdateDebt()
    {
        int currentDebt = ratingManager.GetStats().CurrentDebt;
        debtText.text = $"{currentDebt}$ / {maxDebt}$";
    }

    public void Pay()
    {
        var stats = ratingManager.GetStats();

        int canPay = bank.Get();
        int payment = stats.CurrentDebt;

        if(canPay <= stats.CurrentDebt) 
            payment = canPay;

        bank.Change(-payment);
        stats.CurrentDebt -= payment;

        UpdateDebt();
    }
}
