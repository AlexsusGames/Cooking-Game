using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "Create/Localization")]
public class LocalizationData : ScriptableObject
{
    public List<string> keys = new();
    public List<LocalizationEntry> Localization = new();
    private Dictionary<string, List<string>> localizationMap = new();
    private int RegionId;

    public void ChangeRegionId(int newId) => RegionId = newId; 

    public void CreateMap()
    {
        for (int i = 0; i < Localization.Count; i++)
        {
            localizationMap[keys[i]] = Localization[i].Values;
        }
        UnityEngine.Debug.Log($"[Create localization Map]: loaded {localizationMap.Count} files");
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
            if (Localization[i].Values.Count <= int.Parse(id))
            {
                Localization[i].Values.Add(localization[i]);
            }
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }


    public void UpdateLocalization(string id, List<string> localization, int indexOfStart)
    {
        int indexOfId = int.Parse(id);

        for (int i = indexOfStart; i < localization.Count; i++)
        {
            Localization[i].Values[indexOfId] = localization[i];
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
            if (keys[i] == key)
            {
                index = i;
                Localization[index].Values[int.Parse(id)] = newValue;
            }
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public string GetTranslated(string message)
    {
        if (localizationMap.TryGetValue(message, out var translations) && RegionId < translations.Count)
        {
            return translations[RegionId];
        }
        UnityEngine.Debug.Log($"[Missing Translation: {message}]/[Region index - {RegionId}]");
        return "";
    }

    public void Debug(string id)
    {
        string result = "";

        for (int i = 0; i < Localization.Count; i++)
        {
            result += Localization[i].Values[int.Parse(id)];
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
            Localization[i].Values.RemoveAt(int.Parse(id));
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
