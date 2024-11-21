using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SteamTests : MonoBehaviour
{
    [Inject] private SteamAchievements achievements;

    private void Start()
    {
        achievements.Init();
    }
}
