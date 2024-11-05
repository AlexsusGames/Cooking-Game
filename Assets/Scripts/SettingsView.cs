using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_Dropdown languageDropDown;
    [Inject] private LanguageChanger changer;
    [Inject] private SceneLoader sceneLoader;

    public static SettingsView Instance;

    public void Init()
    {
        Instance = this;

        sceneLoader.SceneChanged += OnSceneChanghed;

        exitButton.onClick.AddListener(() =>
        {
            sceneLoader.LoadMenu();
        });
    }

    private void Start()
    {
        languageDropDown.SetValueWithoutNotify(changer.RegionIndex);
        languageDropDown.onValueChanged.AddListener(changer.ChangeIndex);
    }

    private void OnSceneChanghed(string name)
    {
        bool enabled = name == "Gameplay";
        exitButton.gameObject.SetActive(enabled);
    }
}
