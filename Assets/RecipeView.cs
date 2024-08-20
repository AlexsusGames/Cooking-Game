using System;
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
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Image[] ingridientsImages;
    [SerializeField] private Image checkMark;


    private event Action<RecipeConfig> sellingChanged;
    private RecipeConfig config;

    private bool isSelling;
    public bool IsSelling
    {
        get => isSelling;
        set
        {
            isSelling = value;
            sellingChanged?.Invoke(config);
        }
    }

    public void SetData(RecipeConfig config, bool isSelling)
    {
        cookingPlace.sprite = cookingPlaces[(int)config.CookingPlace];
        foodImage.sprite = config.picture;
        nameText.text = config.Name;
        priceText.text = $"Цена:\n<color=green>{config.Price}$";
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

        this.config = config;
        this.IsSelling = isSelling;
        checkMark.enabled = IsSelling;
    }
    public void ChangeSelling()
    {
        IsSelling = !IsSelling;
        checkMark.enabled = IsSelling;
    }
}
