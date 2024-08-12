using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEnterParams : SceneEnterParams
{
    public string SaveFileName { get; }
    public int LevelNumber { get; }

    public GamePlayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
    {
        SaveFileName = saveFileName;
        LevelNumber = levelNumber;
    }
}
