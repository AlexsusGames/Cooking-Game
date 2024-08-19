using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private Image bar;

    private float Value;
    public bool IsExhausted => Value < 10;
    private WaitForSeconds await = new WaitForSeconds(0.1f);


    private void Awake()
    {
        Value = 100;
        StartCoroutine(HavingRest());
    }

    public void Sprint()
    {
        Value -= 0.05f;

        UpdateBar(Value);
    }

    private void UpdateBar(float barProgress)
    {
        barProgress = Value / 100;
        bar.fillAmount = barProgress;
    }

    private IEnumerator HavingRest()
    {
        while (true)
        {
            if (Value < 100)
            {
                Value += 0.5f;
                UpdateBar(Value);
                yield return await;
            }
            yield return await;
        }
    }
}
