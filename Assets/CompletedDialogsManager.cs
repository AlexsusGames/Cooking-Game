using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedDialogsManager : MonoBehaviour, IProgressDataProvider
{
    private const string KEY = "Completed_dialogs_Data";
    private CompletedDialogsData data;

    public void Save()
    {
        string save = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(KEY, save);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(KEY))
        {
            string save = PlayerPrefs.GetString(KEY);
            data = JsonUtility.FromJson<CompletedDialogsData>(save);
        }
        else data = new();
    }

    public void CompleteDialog(string dialogId) => data.CompletedDialogs.Add(dialogId);
    public bool Has(string dialogId) => data.CompletedDialogs.Contains(dialogId);
}

[System.Serializable]
public class CompletedDialogsData
{
    public List<string> CompletedDialogs = new();
}
