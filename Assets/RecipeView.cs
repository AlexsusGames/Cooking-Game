using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeView : MonoBehaviour
{
    [SerializeField] private Sprite[] cookingPlaces;
    [SerializeField] private Image cookingPlace;
    [SerializeField] private Image foodImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image[] ingridientsImages;

    public void SetData(RecipeConfig config)
    {
        cookingPlace.sprite = cookingPlaces[(int)config.CookingPlace];
        foodImage.sprite = config.picture;
        nameText.text = config.Name;
        descriptionText.text = config.Description;

        for (int i = 0; i < ingridientsImages.Length; i++)
        {
            if(config.Products.Count > i)
            {
                ingridientsImages[i].enabled = true;
                ingridientsImages[i].sprite = config.Products[i].ProductIcon;
            }
            else ingridientsImages[i].enabled = false;
        }
    }
}
