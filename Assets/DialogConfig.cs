using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Create/DialogConfig")]
public class DialogConfig : ScriptableObject
{
    public int requiredDay;
    public DialogData[] Dialog;
}
[System.Serializable]
public class DialogData
{
    public int PersonId;
    public Sprite Icon;
    public string Message;
}
