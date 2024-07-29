using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUIRootBinder sceneUIPrefab;

    public Observable<MenuExitParams> Run(UIRootView uiRoot, MenuEnterParams enterParams)
    {
        var uiScene = Instantiate(sceneUIPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        var sceneExitSignal = new Subject<Unit>();
        uiScene.Bind(sceneExitSignal);

        Debug.Log($"EnterParamsResult: {enterParams?.Results}");

        var gamePlayEnterParams = new GamePlayEnterParams("saveFile", 1);
        var menuExitParams = new MenuExitParams(gamePlayEnterParams);
        var exitToGamePlaySceneSignal = sceneExitSignal.Select(_ => menuExitParams);

        return exitToGamePlaySceneSignal;
    }
}
