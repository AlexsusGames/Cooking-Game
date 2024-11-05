using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private DialogView[] dialogViews;
    private DialogView cachedView;

    private bool IsSkiped;
    public async void ShowDialog(DialogConfig config)
    {
        for (int i = 0; i < config.Dialog.Length; i++)
        {
            if(cachedView != null)
            {
                cachedView.ChangeDialogEnable(false);
                cachedView.SetActivity(false);
            }

            IsSkiped = false;
            Send(config.Dialog[i]);

            while (!IsSkiped)
            {
                await Task.Yield();
            }
        }

        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    private void Send(DialogData data)
    {
        for(int i = 0;i < dialogViews.Length;i++)
        {
            if (dialogViews[i].UpdateView(data))
            {
                dialogViews[i].SetActivity(true);
                cachedView = dialogViews[i];
                return;
            }
        }
    }

    public void ContinueDialog() => IsSkiped = true;
}
