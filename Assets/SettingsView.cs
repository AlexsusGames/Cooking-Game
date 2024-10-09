using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [Inject] private SceneLoader sceneLoader;

    public static SettingsView Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else return;

        sceneLoader.SceneChanged += OnSceneChanghed;

        exitButton.onClick.AddListener(() =>
        {
            sceneLoader.LoadMenu();
        });
    }
    private void OnSceneChanghed(string name)
    {
        bool enabled = name == "Gameplay";
        exitButton.gameObject.SetActive(enabled);
    }
}
