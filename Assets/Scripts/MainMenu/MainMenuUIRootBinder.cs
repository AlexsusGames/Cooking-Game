using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class MainMenuUIRootBinder : MonoBehaviour
{
    private Subject<Unit> exitSceneSignal;


    public void HandleGoToGamePlayButtonClicked()
    {
        exitSceneSignal.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignal)
    {
        this.exitSceneSignal = exitSceneSignal;
    }
}
