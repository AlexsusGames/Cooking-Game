using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LocalizationFinder : MonoBehaviour
{
    [SerializeField] private LocalizationData data;
    [Inject] private LanguageChanger changer;

    private List<ILocalization> resourcesLocalization = new();
    private List<ILocalization> monoLocalization = new();

    public void Init()
    {
        data.ChangeRegionId(changer.RegionIndex);

        changer.OnLocalizationChange += data.ChangeRegionId;
        changer.OnLocalizationChange += OnLocalizationChange;
        SceneManager.activeSceneChanged += OnSceneChanged;

        data.CreateMap();
        Debug.Log($"Localization map created");
    }

    public void CreateSoLocalization()
    {
        LoadSoLocalization();
        Debug.Log($"[Load SO Localization] loaded {resourcesLocalization.Count})");
    }

    private void OnLocalizationChange(int _)
    {
        LoadMonoLocalization();
        for (int i = 0; i < monoLocalization.Count; i++)
        {
            ChangeValues(monoLocalization[i]);
        }
        for (int i = 0; i < resourcesLocalization.Count; i++)
        {
            ChangeValues(resourcesLocalization[i]);
        }
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        LoadMonoLocalization();
    }

    private void LoadMonoLocalization()
    {
        MonoLocalization[] loc = FindObjectsOfType<MonoLocalization>(true);
        monoLocalization.Clear();

        for (int i = 0; i < loc.Length; i++)
        {
            ChangeValues(loc[i]);
            monoLocalization.Add(loc[i]);
        }
    }
    private void LoadSoLocalization()
    {
        SoLocalization[] loc = Resources.LoadAll<SoLocalization>("Localization");

        for (int i = 0; i < loc.Length; i++)
        {
            ChangeValues(loc[i]);
            resourcesLocalization.Add(loc[i]);
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
