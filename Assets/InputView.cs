using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputView : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    [SerializeField] private TMP_Text buttonName;
    [SerializeField] private TMP_Text buttonDescribtion;

    public void Show(string name, string describtion)
    {
        parent.SetActive(true);

        buttonName.text = name;
        buttonDescribtion.text = describtion;
    }

    public void Hide()
    {
        parent.SetActive(false);
    }
}
