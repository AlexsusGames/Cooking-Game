using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestConfig", menuName = "CreateConfig/QuestConfig")]
public class QuestData : SoLocalization
{
    public Sprite QuestIcon;
    public string QuestName;
    public string Describtion;
    public int Progress;
    public int CurrentProgress;
    public bool IsGlobal;
    public string QuestId;
    public string QuestDescribtion => $"{Describtion}\n[{CurrentProgress}/{Progress}]";

    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            CachedKeys = new string[] { QuestName, Describtion };
        }

        return CachedKeys;
    }

    public override void Set(params string[] param)
    {
        QuestName = param[0];
        Describtion = param[1];
    }
}
