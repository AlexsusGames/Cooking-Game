using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Internet : MonoBehaviour
{
    [SerializeField] private GameObject internerWindow;
    [Inject] private WindowController windowController;

    private void OnTriggerExit(Collider other)
    {
        MakeActive(false);
    }

    public void MakeActive(bool isActive)
    {
        Cursor.visible = isActive;

        if (isActive)
        {
            windowController.AddWindow(internerWindow);
        }
        else windowController.CloseWindow(internerWindow);
    }
}
