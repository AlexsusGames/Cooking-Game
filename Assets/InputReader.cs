using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    [SerializeField] private GameObject recipesPanel;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            recipesPanel.SetActive(false);
        }

        if (Input.GetButtonDown("B"))
        {
            recipesPanel.SetActive(!recipesPanel.activeInHierarchy);
        }
    }
}
