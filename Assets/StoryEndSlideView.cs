using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryEndSlideView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    public void SetData(DialogData data)
    {
        image.sprite = data.Icon;
        text.text = data.Message;
    }
}
