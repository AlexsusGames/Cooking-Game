using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GirlController controller;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private NpcActionCommand[] firstCommands;
    [SerializeField] private NpcActionCommand[] secondCommands;
    [SerializeField] private NpcActionCommand sleapCommand;
    [SerializeField] private FeedTable feedTable;
    private int randomDelay => Random.Range(2000, 10000);

    private void Awake()
    {
        lightSystem.OnDayStart += StartDay;
        lightSystem.OnDayEnd += GoTobed;
    }
    private void Start() => GoTobed();

    private async void StartDay()
    {
        await Task.Delay(randomDelay);

        if (feedTable.IsCovered())
            await controller.Commit(secondCommands[0]);

        for (int i = 0; i < firstCommands.Length; i++)
        {
            if (lightSystem.IsOpen)
            {
                await controller.Commit(firstCommands[i]);
            }
            else return;
        }

        await Task.Delay(randomDelay);

        while (!feedTable.IsCovered())
        {
            await Task.Delay(1000);
        }

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
