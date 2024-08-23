using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Internet : MonoBehaviour
{
    [SerializeField] private GameObject internerWindow;

    private void OnTriggerExit(Collider other)
    {
        MakeActive(false);
    }

    public void MakeActive(bool isActive)
    {
        internerWindow.SetActive(isActive);
        Cursor.visible = isActive;
    }
}
