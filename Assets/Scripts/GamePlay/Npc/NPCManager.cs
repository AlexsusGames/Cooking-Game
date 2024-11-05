using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private QuestData girlQuest;
    [SerializeField] private GirlController controller;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private NpcActionCommand[] firstCommands;
    [SerializeField] private NpcActionCommand[] secondCommands;
    [SerializeField] private NpcActionCommand sleapCommand;
    [SerializeField] private NpcActionCommand sitCommand;
    [Inject] private QuestHandler quests;

    private FeedTable feedTable;
    private int randomDelay => Random.Range(2000, 10000);

    private async void Start()
    {
        feedTable = FindObjectOfType<FeedTable>();

        lightSystem.OnDayStart += StartDay;
        lightSystem.OnDayEnd += GoTobed;

        await controller.Commit(sitCommand);
    }

    private async void StartDay()
    {
        await Task.Delay(randomDelay);

        if (feedTable.IsCovered())
        {
            await controller.Commit(secondCommands[0]);
            feedTable.RemoveFood();
        }

        for (int i = 0; i < firstCommands.Length; i++)
        {
            if (lightSystem.IsOpen)
            {
                await controller.Commit(firstCommands[i]);
            }
            else return;
        }

        quests.AddQuest(girlQuest);

        await Task.Delay(randomDelay);

        while (!feedTable.IsCovered())
        {
            await Task.Delay(1000);
        }

        quests.ChangeProgress(girlQuest, 1);

        for (int i = 0;i < secondCommands.Length; i++)
        {
            if(lightSystem.IsOpen)
            {
                await controller.Commit(secondCommands[i]);
                if (i == 0) feedTable.RemoveFood();
            }
            else return;
        }
    }

    private async void GoTobed()
    {
        controller.CancelToken();

        await Task.Yield();
        await controller.Commit(sleapCommand);
    }
}
