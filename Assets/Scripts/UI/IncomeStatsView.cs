using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncomeStatsView : MonoBehaviour
{
    private enum GraphicType { People, Income, Loses };

    [SerializeField] private Image[] stats;
    [SerializeField] private TMP_Text[] amounts;

    [SerializeField] private int maxValue;
    [SerializeField] private GraphicType graphicType;

    private StatsProvider data = new();
    private List<int> values = new();

    private void Awake()
    {
        CreateData();
        CreateGraphic();
    }

    private void CreateData()
    {
        var week = data.GetStats();

        for (int i = 0; i < week.Count; i++)
        {
            switch (graphicType)
            {
                case GraphicType.People: values.Add(week[i].PeopleServed); break;
                case GraphicType.Income: values.Add(week[i].MoneyEarned); break;
                case GraphicType.Loses: values.Add(week[i].MoneySpended); break;
            }
        }
    }

    private void CreateGraphic()
    {
        for (int i = 0;i < values.Count; i++)
        {
            float fillAmount = (float)values[i] / maxValue;
            stats[i].fillAmount = fillAmount;

            amounts[i].text = values[i].ToString();
        }
    }
}
