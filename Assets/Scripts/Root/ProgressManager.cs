using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject[] progressObjects;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private EndGameWindow endGameWindow;
    [SerializeField] private Bank bank;
    [SerializeField] private TaxManager taxes;
    [SerializeField] private PlayerChanger playerChanger;
    [SerializeField] private DialogSystem dialogSystem;
    [Space]
    [Header("StoryEnd")]
    [SerializeField] private StoryEndView storyEndView;
    [SerializeField] private StoryEndConfig badEndConfig;

    [SerializeField] private WorldSettings worldSettings;
    [Inject] private FamilyStateManager familyStateManager;

    private readonly List<IProgressDataProvider> progress = new();
    private readonly PeopleCounter peopleCounter = new();
    private readonly StatsProvider stats = new();

    private void Awake()
    {
        Cursor.visible = false;
        familyStateManager.Load();
        bank.Init();

        worldSettings.Init(playerChanger, familyStateManager.IsHasParent);

        for (int i = 0; i < progressObjects.Length; i++)
        {
            progressObjects[i].TryGetComponent(out IProgressDataProvider progress);
            progress.Load();
            this.progress.Add(progress);   
        }

        TaxCounter.Reset();
        taxes.Load();

        if(!familyStateManager.IsHasParent && !familyStateManager.IsHasGirl)
        {
            storyEndView.ShowEndStory(badEndConfig);
            return;
        }

        bool isHasStory = false;

        if (familyStateManager.IsHasGirl)
        {
            isHasStory = dialogSystem.TryShowStory();
        }
        else worldSettings.ChangeGirlEnable(false);

        if(!isHasStory && StoryProgress.CurrentDay < 10)
        {
            DialogSelector dialogSelector = GetComponent<DialogSelector>();
            dialogSelector.SelectDialog(dialogSystem);
        }
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

            familyStateManager.Save();

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
