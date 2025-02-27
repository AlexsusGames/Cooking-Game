using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SceneContextRoot : MonoBehaviour
{
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject settings;
    [SerializeField] private LocalizationFinder localizationFinder;
    [SerializeField] private SettingsView settingsView;
    [SerializeField] private GameObject controllView;

    public static SceneContextRoot instance = null;

    public bool SettingsMenuEnabled
    {
        get => settings.activeInHierarchy;
        private set => settings.SetActive(value);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            localizationFinder.Init();
            settingsView.Init();
        }
        else Destroy(gameObject);
    }

    private void Start() => localizationFinder.CreateSoLocalization();

    public void ChangeSettingEnable()
    {
        SettingsMenuEnabled = !SettingsMenuEnabled;
        Time.timeScale = SettingsMenuEnabled ? 0f : 1f;

        if(SettingsMenuEnabled == false)
        {
            controllView.SetActive(false);
        }
    }

    public async void ShowLoadScreen()
    {
        if (SettingsMenuEnabled)
            ChangeSettingEnable();

        screen.SetActive(true);
        await Task.Delay(2000);
        screen.SetActive(false);
    }
}
