using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InternerConfirmMenu : MonoLocalization
{
    [SerializeField] private GameObject blockObject;
    [SerializeField] private TMP_Text dealText;
    [SerializeField] private TMP_Text errorText;

    private string[] values = { "Оплатить суму заказа в размере: ", "за доставку?", "Не хватает денег" };

    public override string[] Get()
    {
        if(CachedKeys == null)
        {
            CachedKeys = values;
        }

        return CachedKeys;
    }

    public override void Set(params string[] param) => values = param;

    public void SetDealText(int money)
    {
        gameObject.SetActive(true);
        blockObject.SetActive(true);
        dealText.text = $"{values[0]}<color=green>{money}$</color> + 20$ {values[1]}";
        errorText.text = "";
    }

    public void ShowError()
    {
        errorText.text = values[2];
    }
}
