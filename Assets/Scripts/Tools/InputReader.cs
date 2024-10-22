using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InputReader : MonoBehaviour
{
    [SerializeField] private GameObject recipesPanel;
    [SerializeField] private GameObject internetPanel;
    [SerializeField] private UnityEvent OpenShop;
    [Inject] private WindowController windowController;

    private bool isTutor;
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            var settings = SceneContextRoot.instance;

            if (settings != null && settings.SettingsMenuEnabled)
            {
                settings.ChangeSettingEnable();
                Cursor.visible = false;
                return;
            }

            if (settings != null && !windowController.HasOpenedWindows())
            {
                settings.ChangeSettingEnable();
                Cursor.visible = true;
                return;
            }

            windowController.CloseLastWindow();

            bool hasOpened = windowController.HasOpenedWindows();
            Cursor.visible = hasOpened;
        }

        if (Input.GetButtonDown("B"))
        {
            bool activeWindow = recipesPanel.activeInHierarchy;
            
            if (activeWindow)
            {
                windowController.CloseWindow(recipesPanel);
            }
            else windowController.AddWindow(recipesPanel);

            Cursor.visible = !activeWindow;
        }

        if (Input.GetButtonDown("R") && !isTutor)
        {
            OpenShop?.Invoke();
        }
    }

    public void TutorEnabled(bool value) => isTutor = value;
}
