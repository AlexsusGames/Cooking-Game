using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DrinkFridgePresenter : MonoBehaviour
{
    [SerializeField] private Image previewedImage;

    [SerializeField] private TMP_Text ratingText;
    [SerializeField] private TMP_Text nameText;

    [SerializeField] private CurrentPartershipView partershipView;

    [Inject] private RatingManager ratingManager;

    private DrinkFridgeConfig previewedConfig;
    private bool isAvailable;

    public void Preview(DrinkFridgeConfig config, int currentRating)
    {
        gameObject.SetActive(true);

        ratingText.text = config.RequiredRating.ToString();
        nameText.text = config.name;
        previewedImage.sprite = config.Icon;

        previewedConfig = config;

        isAvailable = previewedConfig.RequiredRating <= currentRating;
    }

    public void Deal()
    {
        if (isAvailable)
        {
            partershipView.SetData(previewedConfig.DrinkType);
            ratingManager.SetRatingFridge(previewedConfig);

            gameObject.SetActive(false);
        }
    }

    private void OnDisable() => gameObject.SetActive(false);
}
