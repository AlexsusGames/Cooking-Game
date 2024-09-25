using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GirlController controller;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private NpcActionCommand[] command;
    [SerializeField] private NpcActionCommand sleapCommand;

    private void Awake()
    {
        lightSystem.OnDayStart += StartDay;
        lightSystem.OnDayEnd += GoTobed;
    }
    private void Start() => GoTobed();

    private async void StartDay()
    {
        for (int i = 0; i < command.Length; i++)
        {
            await controller.Commit(command[i]);
        }
    }

    private async void GoTobed()
    {
        controller.CancelToken();

        await Task.Yield();
        await controller.Commit(sleapCommand);
    }
}
