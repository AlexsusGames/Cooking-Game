using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader 
{
    private const string GAMEPLAY = "Gameplay";
    private const string MAIN_MENU = "MainMenu";

    public event Action<string> SceneChanged;

    public void LoadGame() => LoadScene(GAMEPLAY);
    public void LoadMenu() => LoadScene(MAIN_MENU);

    private async void LoadScene(string name)
    {
        SceneContextRoot.instance.ShowLoadScreen();
        await Task.Delay(1000);
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        SceneChanged?.Invoke(name);
    }

    public string GetCurrentScene() => SceneManager.GetActiveScene().name;
}
