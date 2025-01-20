using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Image slotColor;

    public void SetData(Sprite slotImage, Color slotColor)
    {
        this.slotImage.sprite = slotImage;
        this.slotColor.color = slotColor;
    }
}
