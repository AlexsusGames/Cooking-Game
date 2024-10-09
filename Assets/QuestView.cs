using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text questName;
    [SerializeField] private TMP_Text questDescription;

    [SerializeField] private Image completedQuestIcon;

    public void UpdateView(QuestData data)
    {
        icon.sprite = data.QuestIcon;
        questName.text = data.QuestName;
        questDescription.text = data.QuestDescribtion;
    }

    public async void Complete(Action callBacl)
    {
        completedQuestIcon.gameObject.SetActive(true);

        while(questName.color.a > 0)
        {
            questName.color = new Color(questName.color.r, questName.color.g, questName.color.b, questName.color.a - 0.002f);
            questDescription.color = new Color(questDescription.color.r, questDescription.color.g, questDescription.color.b, questDescription.color.a - 0.002f);
            await Task.Yield();
        }

        Destroy(gameObject);
        callBacl?.Invoke();
    }
}
