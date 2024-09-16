using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IncomeStats
{
    public List<DayStatsData> LastWeek = new();
}

[System.Serializable] 
public class DayStatsData
{
    public int PeopleServed;
    public int MoneyEarned;
    public int MoneySpended;
}
