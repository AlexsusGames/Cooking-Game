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
    [Inject] private LanguageChanger languageChanger;
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

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            var settings = SceneContextRoot.instance;
            settings.ChangeSettingEnable();
        }
    }

    public void StartNewGame()
    {
        int regionId = languageChanger.RegionIndex;
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        PlayerPrefs.SetString(Key, "");
        languageChanger.ChangeIndexWithoutNotify(regionId);
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
