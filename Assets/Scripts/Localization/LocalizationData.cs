using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "Create/Localization")]
public class LocalizationData : ScriptableObject
{
    public List<string> keys = new();
    private List<string> regionId = new List<string> { "ru", "en" };
    public List<LocalizationEntry> Localization = new();
    private Dictionary<string, List<string>> localizationMap = new();

    public void CreateMap()
    {
        for (int i = 0; i < Localization.Count; i++)
        {
            localizationMap[keys[i]] = Localization[i].Values;
        }
    }

    public void CreateKeys(List<string> localization)
    {
        for (int i = 0; i < localization.Count; i++)
        {
            Localization.Add(new LocalizationEntry());

            Localization[i].Values.Add(localization[i]);
            keys.Add(localization[i]);
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void CreateLocalization(string id, List<string> localization)
    {
        for (int i = 0; i < localization.Count; i++)
        {
            if (Localization[i].Values.Count <= regionId.IndexOf(id))
            {
                Localization[i].Values.Add(localization[i]);
            }
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void EditString(string id, string key, string newValue)
    {
        Debug();
        int index = 0;

        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] == key) index = i;
        }

        Localization[index].Values[regionId.IndexOf(id)] = newValue;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public string GetTranslated(string message)
    {
        int regionId = 1;


        return localizationMap[message][regionId];
    }

    public void Debug(string id)
    {
        string result = "";

        for (int i = 0; i < Localization.Count; i++)
        {
            result += Localization[i].Values[regionId.IndexOf(id)];
        }

        UnityEngine.Debug.Log(result);
    }

    public void Debug()
    {
        string result = "";

        for (int i = 0; i < Localization.Count; i++)
        {
            result += Localization[i].Values[0];
        }

        UnityEngine.Debug.Log(result);
    }

    public void Clear(string id)
    {
        for (int i = 0;i < Localization.Count; i++)
        {
            Localization[i].Values.RemoveAt(regionId.IndexOf(id));
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void Clear()
    {
        Localization.Clear();
        keys.Clear();

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public int GetLocCount()
    {
        return Localization.Count;
    }
}
