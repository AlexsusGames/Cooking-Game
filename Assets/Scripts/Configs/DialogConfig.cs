using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Create/DialogConfig")]
public class DialogConfig : SoLocalization
{
    public int requiredDay;
    public DialogData[] Dialog;

    public override string[] Get()
    {
        if(CachedKeys == null)
        {
            List<string> list = new();

            for (int i = 0; i < Dialog.Length; i++)
            {
                list.Add(Dialog[i].Message);
            }

            CachedKeys = list.ToArray();
        }

        return CachedKeys;
    }

    public override void Set(params string[] param)
    {
        for (int i = 0; i < Dialog.Length; i++)
        {
            int index = i;
            Dialog[index].Message = param[index];
        }
    }
}
[System.Serializable]
public class DialogData
{
    public int PersonId;
    public Sprite Icon;
    public string Message;
}
