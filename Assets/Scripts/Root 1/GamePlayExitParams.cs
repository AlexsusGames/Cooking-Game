using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayExitParams 
{
    public MenuEnterParams EnterParams { get; }

    public GamePlayExitParams(MenuEnterParams enterParams)
    {
        EnterParams = enterParams;
    }
}
