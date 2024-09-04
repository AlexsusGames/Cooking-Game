using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    [SerializeField] private GameObject recipesPanel;
    [SerializeField] private GameObject internetPanel;
    [SerializeField] private UnityEvent OpenShop;

    private void Awake() => Cursor.visible = false;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            recipesPanel.SetActive(false);
            internetPanel.SetActive(false);
            Cursor.visible = false;
        }

        if (Input.GetButtonDown("B"))
        {
            bool activeWindow = recipesPanel.activeInHierarchy;
            recipesPanel.SetActive(!activeWindow);
            Cursor.visible = !activeWindow;
        }

        if (Input.GetButtonDown("R"))
        {
            OpenShop?.Invoke();
        }
    }
}
