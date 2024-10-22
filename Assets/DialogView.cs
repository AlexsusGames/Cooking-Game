using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogView : MonoBehaviour
{
    [SerializeField] private Image activeImage;
    [SerializeField] private Image inactiveImage;
    [SerializeField] private GameObject dialogTab;
    [SerializeField] private TMP_Text messageText;

    private int PersonID;

    public bool UpdateView(DialogData data)
    {
        if(data.PersonId == PersonID || PersonID == 0)
        {
            activeImage.sprite = data.Icon;
            inactiveImage.sprite = data.Icon;
            messageText.text = data.Message;
            PersonID = data.PersonId;

            ChangeDialogEnable(true);
            return true;
        }
        else return false;
    }

    public void SetActivity(bool value)
    {
        activeImage.gameObject.SetActive(value);
        inactiveImage.gameObject.SetActive(!value);
    }
    public void ChangeDialogEnable(bool value) => dialogTab.SetActive(value);
}
