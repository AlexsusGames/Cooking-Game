using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class LocalizationWindow : EditorWindow
{
    private LocalizationData data;
    private string id;
    private int indexToStart;
    private string stringToEdit;
    private string newValue;

    private List<string> localization = new();
    private int loadedLocalizations;
    private LibreTranslate translator = new();
    private List<SoLocalization> soLocalizations = new();

    private bool isBusy = false;

    [MenuItem("Window/Localization")]
    public static void Init()
    {
        LocalizationWindow window = GetWindow<LocalizationWindow>("Localization");
        window.minSize = new Vector2(350, 450);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("Localization Data", EditorStyles.boldLabel);
        data = (LocalizationData)EditorGUILayout.ObjectField("Data Source:", data, typeof(LocalizationData), true);
        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Region ID", EditorStyles.label);
        id = EditorGUILayout.TextField(id);

        EditorGUILayout.LabelField($"Loaded Localizations: {loadedLocalizations}");

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Load Localizations", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Resources", GUILayout.Width(150)))
        {
            LoadResourcesLocalization();
        }
        if (GUILayout.Button("Load Mono", GUILayout.Width(150)))
        {
            LoadMonoLocalization();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Clear Actions", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear by ID", GUILayout.Width(100)))
        {
            data.Clear(id);
        }
        if (GUILayout.Button("Clear All", GUILayout.Width(100)))
        {
            data.Clear();
        }
        if (GUILayout.Button("Clear Data", GUILayout.Width(100)))
        {
            loadedLocalizations = 0;
            localization.Clear();
            soLocalizations.Clear();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Create Localization", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create Keys", GUILayout.Width(100)))
        {
            data.CreateKeys(localization);
            ResetLocalizationData();
        }
        if (GUILayout.Button("Create by ID", GUILayout.Width(100)) && !isBusy)
        {
            _ = CreateLocalization(new List<string>(data.keys));
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("Start Index", EditorStyles.label);
        indexToStart = EditorGUILayout.IntField(indexToStart);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Continue by ID", GUILayout.Width(150)) && !isBusy)
        {
            _ = CreateLocalization(new List<string>(data.keys), indexToStart);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Edit Localization", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Edit Key", EditorStyles.label);
        stringToEdit = EditorGUILayout.TextField(stringToEdit);

        EditorGUILayout.LabelField("New Value", EditorStyles.label);
        newValue = EditorGUILayout.TextField(newValue);

        if (GUILayout.Button("Apply Edit", GUILayout.Width(150)))
        {
            data.EditString(id, stringToEdit, newValue);
        }

        EditorGUILayout.Space(5);

        if (GUILayout.Button("Print Localization by ID", GUILayout.Width(200)))
        {
            data.Debug(id);
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("SO Actions", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create keys", GUILayout.Width(100)))
        {
            CreateSoKeys();
        }
        if (GUILayout.Button("Clear keys", GUILayout.Width(100)))
        {
            for (int i = 0; i < soLocalizations.Count; i++)
            {
                soLocalizations[i].ClearKey();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void LoadResourcesLocalization()
    {
        SoLocalization[] loc = Resources.LoadAll<SoLocalization>("Localization");
        Debug.Log("loaded " +  loc.Length);
        foreach (var localizationObject in loc)
        {
            localization.AddRange(localizationObject.Get());
            loadedLocalizations += localizationObject.Get().Length;
            soLocalizations.Add(localizationObject);
            Debug.Log(loadedLocalizations);
        }
    }

    private void LoadMonoLocalization()
    {
        MonoLocalization[] loc = FindObjectsOfType<MonoLocalization>(true);
        foreach (var localizationObject in loc)
        {
            localization.AddRange(localizationObject.Get());
            loadedLocalizations += localizationObject.Get().Length;
        }
    }

    private void ResetLocalizationData()
    {
        loadedLocalizations = 0;
        localization.Clear();
        soLocalizations.Clear();
    }

    private void CreateSoKeys()
    {
        for (int i = 0; i < soLocalizations.Count; i++)
        {
            var key = soLocalizations[i].Get();
            soLocalizations[i].CreateKey(key);
        }
    }

    public async Task CreateLocalization(List<string> keys)
    {
        isBusy = true;
        var translated = new List<string>();
        for (int i = 0; i < keys.Count; i++)
        {
            string result = await translator.TranslateText(keys[i], "ru", id);
            Debug.Log($"{i + 1}/{keys.Count} created");
            translated.Add(result);
        }
        data.CreateLocalization(id, translated);
        isBusy = false;
    }

    public async Task CreateLocalization(List<string> keys, int index)
    {
        isBusy = true;
        var translated = new List<string>();
        for (int i = index; i < keys.Count; i++)
        {
            string result = await translator.TranslateText(keys[i], "ru", id);
            Debug.Log(keys[i] + $"Index: {i}");
            Debug.Log($"{i + 1}/{keys.Count} created");
            translated.Add(result);
        }
        data.UpdateLocalization(id, translated, index);
        isBusy = false;
    }
}
