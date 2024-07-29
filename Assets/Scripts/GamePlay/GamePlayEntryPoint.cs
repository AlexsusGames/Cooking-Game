using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    [SerializeField] private UIGameplayRootBinder sceneUIPrefab;

    public Observable<GamePlayExitParams> Run(UIRootView uiRoot, GamePlayEnterParams enterParams)
    {
        var uiScene = Instantiate(sceneUIPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var exitSceneSignalSubj = new Subject<Unit>();

        uiScene.Bind(exitSceneSignalSubj);

        var mainMenuEnterParams = new MenuEnterParams("test");
        var exitParams = new GamePlayExitParams(mainMenuEnterParams);
        var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

        Debug.Log($"SaveFile: {enterParams.SaveFileName}, Level: {enterParams.LevelNumber}");
        return exitToMainMenuSceneSignal;
    }
}
