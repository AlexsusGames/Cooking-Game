using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DrinkFridgeView : MonoBehaviour
{
    [SerializeField] private Image fridgeImage;
    [SerializeField] private TMP_Text requiredRatingText;
    private Button button;

    public void Init(DrinkFridgeConfig config)
    {
        button = GetComponent<Button>();

        fridgeImage.sprite = config.Icon;
        requiredRatingText.text = config.RequiredRating.ToString();
    }

    public void AssingListener(UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}
