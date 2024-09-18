using UnityEngine;

public class PeopleCounter
{
    private const string Key = "People_Count_Key";

    public int GetCount()
    {
        return PlayerPrefs.GetInt(Key);
    }

    public void ChangeCount(int value)
    {
        var count = GetCount();
        PlayerPrefs.SetInt(Key, count + value);
    }
}
