using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayEntryPoint : MonoBehaviour
{
    public Action GoToMainMenuSceneRequested;

    [SerializeField] private UIGameplayRootBinder sceneUIPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(sceneUIPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.OnBackToMenuButtonClicked += () =>
        {
            GoToMainMenuSceneRequested?.Invoke();
        };
    }
}
