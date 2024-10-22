using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestConfig", menuName = "CreateConfig/QuestConfig")]
public class QuestData : ScriptableObject
{
    public Sprite QuestIcon;
    public string QuestName;
    public string Describtion;
    public int Progress;
    public int CurrentProgress;
    public bool IsGlobal;
    public string QuestId;
    public string QuestDescribtion => $"{Describtion}\n[{CurrentProgress}/{Progress}]";
}
