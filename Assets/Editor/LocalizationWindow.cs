using log4net.Appender;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class LocalizationWindow : EditorWindow
{
    private LocalizationData data;
    private string id;
    private string stringToEdit;
    private string newValue;

    private List<string> localization = new();
    private int loadedLocalizations;
    private LibreTranslate translator = new();

    private bool isBusy = false;

    [MenuItem("Window/Localization")]
    public static void Init()
    {
        LocalizationWindow window = GetWindow<LocalizationWindow>("Localization");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(10);
        data = (LocalizationData)EditorGUILayout.ObjectField(data, typeof(LocalizationData), true);
        EditorGUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("RegionId");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        id = EditorGUILayout.TextField(id);
        EditorGUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label($"Current loaded: {loadedLocalizations}");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Get Resources localization", GUILayout.Width(300), GUILayout.Height(50)))
        {
            SoLocalization[] loc = Resources.LoadAll<SoLocalization>("");

            Debug.Log($"LOCALIZATION: Founded SOLocalization: {loc.Length}.");

            for (int i = 0; i < loc.Length; i++)
            {
                var texts = loc[i].Get();

                for (int j = 0; j < texts.Length; j++)
                {
                    localization.Add(texts[j]);
                    loadedLocalizations++;
                }
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Get Mono localization", GUILayout.Width(300), GUILayout.Height(50)))
        {
            MonoLocalization[] loc = FindObjectsOfType<MonoLocalization>(true);

            Debug.Log($"LOCALIZATION: Founded MonoLocalization: {loc.Length}.");

            for (int i = 0; i < loc.Length; i++)
            {
                var texts = loc[i].Get();

                for (int j = 0; j < texts.Length; j++)
                {
                    localization.Add(texts[j]);
                    loadedLocalizations++;
                }
            }

            Debug.Log($"LOCALIZATION: Total Count: {localization.Count}");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear localization by ID", GUILayout.Width(200), GUILayout.Height(40)))
        {
            data.Clear(id);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear all localization", GUILayout.Width(200), GUILayout.Height(40)))
        {
            data.Clear();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear this data", GUILayout.Width(200), GUILayout.Height(40)))
        {
            loadedLocalizations = 0;
            localization.Clear();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create localization keys", GUILayout.Width(200), GUILayout.Height(40)))
        {
            Debug.Log("Keys created");
            data.CreateKeys(localization);
            loadedLocalizations = 0;
            localization.Clear();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create localization by ID", GUILayout.Width(200), GUILayout.Height(40)))
        {
            if(!isBusy)
            {
                List<string> list = new List<string>(data.keys);

                _ = CreateLocalization(list);
            }
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Print localization by ID", GUILayout.Width(200), GUILayout.Height(40)))
        {
            data.Debug(id);
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Label("Editting: key");

        stringToEdit = EditorGUILayout.TextField(stringToEdit);

        GUILayout.Label("Editting: newValue");

        newValue = EditorGUILayout.TextField(newValue);

        if (GUILayout.Button("Edit", GUILayout.Width(100), GUILayout.Height(20)))
        {
            data.EditString(id, stringToEdit, newValue);
        }
    }

    public async Task CreateLocalization(List<string> keys)
    {
        Debug.Log("Start creating");

        isBusy = true;

        var regionId = id;
        var source = "ru";
        List<string> translated = new();

        for (int i = 0; i < keys.Count; i++)
        {
            string result = await translator.TranslateText(keys[i], source, regionId);
            Debug.Log($"{i + 1}/{keys.Count} created");
            translated.Add(result);
        }

        data.CreateLocalization(id, translated);


        Debug.Log("Localization created");
        isBusy = false;
    }
}
