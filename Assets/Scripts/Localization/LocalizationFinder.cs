using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalizationFinder : MonoBehaviour
{
    [SerializeField] private LocalizationData data;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        data.CreateMap();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void Start()
    {
        LoadSoLocalization();
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        LoadMonoLocalization();
    }

    private void LoadMonoLocalization()
    {
        MonoLocalization[] loc = FindObjectsOfType<MonoLocalization>(true);

        for (int i = 0; i < loc.Length; i++)
        {
            ChangeValues(loc[i]);
        }
    }
    private void LoadSoLocalization()
    {
        SoLocalization[] loc = Resources.LoadAll<SoLocalization>("");

        for (int i = 0; i < loc.Length; i++)
        {
            ChangeValues(loc[i]);
        }
    }

    private void ChangeValues(ILocalization loc)
    {
        var keys = loc.Get();
        string[] newValues = new string[keys.Length];

        for (int i = 0; i < keys.Length; i++)
        {
            newValues[i] = data.GetTranslated(keys[i]);
        }

        loc.Set(newValues);
    }
}
