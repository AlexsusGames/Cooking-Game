using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoLocalization : ScriptableObject, ILocalization
{
    public string[] CachedKeys;
    public bool Ignore;

    public void CreateKey(string[] values)
    {
        CachedKeys = new string[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            CachedKeys[i] = values[i];
        }

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public void ClearKey()
    {
        CachedKeys = null;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    public abstract string[] Get();
    public abstract void Set(params string[] param);
}
