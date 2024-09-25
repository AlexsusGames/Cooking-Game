using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject[] progressObjects;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private EndGameWindow endGameWindow;
    [SerializeField] private Bank bank;
    [SerializeField] private TaxManager taxes;

    private readonly List<IProgressDataProvider> progress = new();
    private readonly PeopleCounter peopleCounter = new();
    private readonly StatsProvider stats = new();

    private void Awake()
    {
        for (int i = 0; i < progressObjects.Length; i++)
        {
            progressObjects[i].TryGetComponent(out IProgressDataProvider progress);
            progress.Load();
            this.progress.Add(progress);   
        }

        TaxCounter.Reset();
        taxes.Load();
    }

    public bool EndDay()
    {
        if(!lightSystem.IsOpen && !lightSystem.isDayTime)
        {
            bank.SaveMoney();
            peopleCounter.ChangeCount(TaxCounter.PeopleServed);

            taxes.Change(TaxCounter.GetTaxes());
            taxes.Save();

            UpdateStats();
            endGameWindow.Open();

            for (int i = 0; i < progress.Count; i++)
            {
                progress[i].Save();
            }

            return true;
        }
        return false;
    }

    private void UpdateStats()
    {
        DayStatsData dayStatsData = new DayStatsData()
        {
            MoneyEarned = TaxCounter.Income,
            MoneySpended = bank.GetLoses(),
            PeopleServed = TaxCounter.PeopleServed
        };

        stats.AddDayToStats(dayStatsData);
    }
}
