using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamAchievements 
{
    public string ACH_DISH_5 = nameof(ACH_DISH_5);
    public string ACH_DISH_10 = nameof(ACH_DISH_10);
    public string ACH_DISH_15 = nameof(ACH_DISH_15);
    public string ACH_DISH_20 = nameof(ACH_DISH_20);
    public string ACH_DISH_ALL = nameof(ACH_DISH_ALL);
    public string ACH_TAX = nameof(ACH_TAX);
    public string ACH_CM = nameof(ACH_CM);
    public string ACH_TABLE = nameof(ACH_TABLE);
    public string ACH_BB = nameof(ACH_BB);
    public string ACH_FRIDGE = nameof(ACH_FRIDGE);
    public string ACH_ALLDEV = nameof(ACH_ALLDEV);
    public string ACH_SERVICE = nameof(ACH_SERVICE);
    public string ACH_STORY = nameof(ACH_STORY);
    public string ACH_CURE = nameof(ACH_CURE);
    public string ACH_SPOILT = nameof(ACH_SPOILT);
    public string ACH_UPGRADE = nameof(ACH_UPGRADE);
    public string ACH_DELIVER = nameof(ACH_DELIVER);

    private bool isSteamStatLoaded;

    public void Init()
    {
        Debug.Log("Initial status - " + SteamManager.Initialized);
        isSteamStatLoaded = Steamworks.SteamUserStats.RequestCurrentStats();
    }

    public void CheckBoughtDevices(string deviceName)
    {
        switch (deviceName)
        {
            case "BoomBox": TrySetAchievement(ACH_BB); break;
            case "CoffeeMachine": TrySetAchievement(ACH_CM); break;
            case "FoodKeeper": TrySetAchievement(ACH_TABLE); break;
            case "Fridge": TrySetAchievement(ACH_FRIDGE); break;
        }
    }

    public void CheckDishCountAchievements(int count)
    {
        if (count >= 5) TrySetAchievement(ACH_DISH_5);
        if (count >= 10) TrySetAchievement(ACH_DISH_10);
        if (count >= 15) TrySetAchievement(ACH_DISH_15);
        if (count >= 20) TrySetAchievement(ACH_DISH_20);
        if (count == 28) TrySetAchievement(ACH_DISH_ALL);
    }

    public void TrySetAchievement(string name)
    {
        if(isSteamStatLoaded)
        {
            bool isAchieved = Steamworks.SteamUserStats.SetAchievement(name);

            if (isAchieved)
            {
                Debug.Log($"Achievement {name} unlocked!");
                Steamworks.SteamUserStats.StoreStats();
            }
            else
            {
                Debug.Log($"Achievement {name} has already unlocked or wasn't found.");
            }
        }
    }
}
