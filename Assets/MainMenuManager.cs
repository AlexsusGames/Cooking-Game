using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Zenject;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button gameContinue;
    private SceneLoader sceneLoader;
    private const string Key = "Save";

    private void Awake() => gameContinue.interactable = PlayerPrefs.HasKey(Key);
    private void OnEnable() => Cursor.visible = true;

    [Inject]
    public void Construct(SceneLoader loader)
    {
        sceneLoader = loader;
        gameContinue.onClick.AddListener(sceneLoader.LoadGame);
    }

    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        PlayerPrefs.SetString(Key, "");
        sceneLoader.LoadGame();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenSettingsMenu()
    {
        SceneContextRoot.instance.ChangeSettingEnable();
    }
}
