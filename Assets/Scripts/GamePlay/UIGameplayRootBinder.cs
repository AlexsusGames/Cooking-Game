using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameplayRootBinder : MonoBehaviour
{
    public Subject<Unit> exitSceneSignalSubj;


    public void HandleGoToMenuButtonClicked()
    {
        exitSceneSignalSubj.OnNext(Unit.Default);
    }

    public void Bind(Subject<Unit> exitSceneSignalSubj)
    {
        this.exitSceneSignalSubj = exitSceneSignalSubj;
    }
}
