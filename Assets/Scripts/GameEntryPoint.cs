using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            coroutines.StartCoroutine(LoadAndStartGamePlay());
            return;
        }
        if(sceneName != Scenes.BOOT)
        {
            return;
        }
#endif
        coroutines.StartCoroutine(LoadAndStartGamePlay());
    }
    private IEnumerator LoadAndStartGamePlay()
    {
        uiRoot.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.GAMEPLAY);

        yield return new WaitForSeconds(2);

        var sceneEntryPoint = Object.FindAnyObjectByType<GamePlayEntryPoint>();
        sceneEntryPoint.Run();

        uiRoot.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
