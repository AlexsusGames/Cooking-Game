using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuEntryPoint : MonoBehaviour
{
    public Action GoToGamePlaySceneRequested;

    [SerializeField] private MainMenuUIRootBinder sceneUIPrefab;

    public void Run(UIRootView uiRoot)
    {
        var uiScene = Instantiate(sceneUIPrefab);
        uiRoot.AttachSceneUI(uiScene.gameObject);

        uiScene.OnGoToGamePlayButtonClicked += () =>
        {
            GoToGamePlaySceneRequested?.Invoke();
        };
    }
}
