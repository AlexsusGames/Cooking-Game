using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using R3;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private Coroutines coroutines;
    private UIRootView uiRoot;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void AutoStartGame()
    {
        instance = new GameEntryPoint();
        instance.RunGame();
    }

    private GameEntryPoint()
    {
        coroutines = new GameObject("Coroutines").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoor = Resources.Load<UIRootView>("UIRootView");
        uiRoot = Object.Instantiate(prefabUIRoor);
        Object.DontDestroyOnLoad(uiRoot.gameObject);
    }

    private void RunGame()
    {
#if UNITY_EDITOR
        var sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == Scenes.GAMEPLAY)
        {
            var enterParams = new GamePlayEnterParams("save1", 0);
            coroutines.StartCoroutine(LoadAndStartGamePlay(enterParams));
            return;
        }
        if (sceneName == Scenes.MENU)
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu());
            return;
        }
        if (sceneName != Scenes.BOOT)
        {
            return;
        }
#endif
        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }
    private IEnumerator LoadAndStartGamePlay(GamePlayEnterParams enterParams)
    {
        uiRoot.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAMEPLAY);

        yield return new WaitForSeconds(2);

        var sceneEntryPoint = Object.FindAnyObjectByType<GamePlayEntryPoint>();
        sceneEntryPoint.Run(uiRoot, enterParams).Subscribe(gameplaExitParams =>
        {
            coroutines.StartCoroutine(LoadAndStartMainMenu(gameplaExitParams.EnterParams));
        });

        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartMainMenu(MenuEnterParams enterParams = null)
    {
        uiRoot.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MENU);

        yield return new WaitForSeconds(2);

        var sceneEntryPoint = Object.FindAnyObjectByType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(uiRoot, enterParams).Subscribe(menuExitParams =>
        {
            var targetSceneParams = menuExitParams.TargetSceneEnterParams.SceneName;

            if(targetSceneParams == Scenes.GAMEPLAY)
            {
                coroutines.StartCoroutine(LoadAndStartGamePlay(menuExitParams.TargetSceneEnterParams.As<GamePlayEnterParams>()));
            }
        });


        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
