using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryEndSlideView : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;
    private Sprite cathedSprite;

    public void SetData(DialogData data)
    {
        image.sprite = data.Icon;
        text.text = data.Message;

        if(cathedSprite != data.Icon)
        {
            image.gameObject.SetActive(false);
            image.gameObject.SetActive(true);

            cathedSprite = data.Icon;
        }
    }
}
