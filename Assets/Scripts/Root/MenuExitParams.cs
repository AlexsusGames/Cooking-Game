using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExitParams 
{
    public SceneEnterParams TargetSceneEnterParams { get; }

    public MenuExitParams(SceneEnterParams enterParams)
    {
        TargetSceneEnterParams = enterParams;
    }
}
