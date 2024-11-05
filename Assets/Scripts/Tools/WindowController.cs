using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController 
{
    private List<GameObject> windows;

    public WindowController()
    {
        windows = new List<GameObject>();
    }

    public void AddWindow(GameObject window)
    {
        if(!windows.Contains(window))
        {
            windows.Add(window);
        }

        window.SetActive(true);
        Cursor.visible = true;
    }

    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);

        if(windows.Contains(window))
        {
            windows.Remove(window);
        }

        if (windows.Count > 0) Cursor.visible = true;
        else Cursor.visible = false;
    }

    public void CloseLastWindow()
    {
        if(windows.Count > 0)
        {
            var lastWindow = windows[windows.Count - 1];

            windows.RemoveAt(windows.Count - 1);

            if (lastWindow.activeInHierarchy)
            {
                lastWindow.SetActive(false);
            }
            else CloseLastWindow();
        }
    }

    public bool HasOpenedWindows()
    {
        for(int i = 0; i < windows.Count; i++)
        {
            if (windows[i].activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }
}
