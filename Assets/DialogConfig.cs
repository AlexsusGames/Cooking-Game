using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Create/DialogConfig")]
public class DialogConfig : ScriptableObject
{
    public int requiredDay;
    public DialogData[] Dialog;

    //public override string[] Get()
    //{
    //    List<string> list = new();

    //    for (int i = 0; i < Dialog.Length; i++)
    //    {
    //        list.Add(Dialog[i].Message);
    //    }

    //    return list.ToArray();
    //}

    //public override void Set(params string[] param)
    //{
    //    for (int i = 0;i < Dialog.Length; i++)
    //    {
    //        Dialog[i].Message = param[i];
    //    }
    //}
}
[System.Serializable]
public class DialogData
{
    public int PersonId;
    public Sprite Icon;
    public string Message;
}
