using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RatingManager : MonoBehaviour, IProgressDataProvider
{
    [SerializeField] private UnmovableDeviceConfig[] configs;
    [Inject] private DeviceDataProvider deviceDataProvider;

    private const string KEY = "Rating_Save";
    private RatingStats stats;

    public void SetRatingFridge(DrinkFridgeConfig config)
    {
        RemoveAllFridges();

        stats.CurrentRatingFridge = config.name;
        deviceDataProvider.AddDevice(config.name, config.StandartPosition);
    }

    public void RemoveAllFridges()
    {
        for (int i = 0; i < configs.Length; i++)
        {
            deviceDataProvider.RemovedDevice(configs[i].name);
        }
    }

    public RatingStats GetStats() => stats;
    public void RiseDebt(DrinkType debtType, bool isSold)
    {
        int cost = 0;
        switch (debtType)
        {
            case DrinkType.Bubble: cost = 5; break;
            case DrinkType.CitrusBoom: cost = 7; break;
            case DrinkType.FizzUp: cost = 10; break;
        }

        cost = isSold ? cost : cost * -1;
        stats.CurrentDebt += cost;
    }
    public bool IsMaxDebt() => stats.CurrentDebt > 200;

    public void Load()
    {
        if (PlayerPrefs.HasKey(KEY))
        {
            string save = PlayerPrefs.GetString(KEY);
            stats = JsonUtility.FromJson<RatingStats>(save);
        }
        else stats = new();
    }

    public void Save()
    {
        string save = JsonUtility.ToJson(stats);
        PlayerPrefs.SetString(KEY, save);
    }
}
public class RatingStats
{
    public int PerfectReviews;
    public int GoodReviews;
    public int NormalReviews;
    public int BadReviews;

    public string CurrentRatingFridge;
    public int CurrentDebt;
}
