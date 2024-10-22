using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private DialogWindow dialogWindow;
    [Inject] private CompletedDialogsManager completedDialogs;
    private DialogConfig[] configs;

    private void Awake() => configs = Resources.LoadAll<DialogConfig>("Dialogs");

    public bool TryShowStory()
    {
        int currentDay = StoryProgress.CurrentDay;

        for (int i = 0; i < configs.Length; i++)
        {
            if (configs[i].requiredDay == currentDay)
            {
                OpenDialogWindow(configs[i]);
                return true;
            }
        }
        return false;
    }

    public bool TryOpenDialog(DialogConfig config)
    {
        if(!completedDialogs.Has(config.name))
        {
            OpenDialogWindow(config);
            return true;
        }

        return false;
    }

    private void OpenDialogWindow(DialogConfig config)
    {
        completedDialogs.CompleteDialog(config.name);
        dialogWindow.gameObject.SetActive(true);
        Cursor.visible = true;
        dialogWindow.ShowDialog(config);
    }
}
