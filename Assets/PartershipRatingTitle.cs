using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PartershipRatingTitle : MonoBehaviour
{
    [SerializeField] private TMP_Text perfectReviews;
    [SerializeField] private TMP_Text goodReviews;
    [SerializeField] private TMP_Text normalReviews;
    [SerializeField] private TMP_Text badReviews;

    [SerializeField] private TMP_Text ratingText;
    [SerializeField] private Slider ratingSlider;

    [Inject] private RatingManager ratingManager;

    public int Rating { get; private set; }

    private void OnEnable()
    {
        SetData();
    }

    private void SetData()
    {
        var data = ratingManager.GetStats();
        int rating = CalculateRating(data);

        ratingText.text = rating.ToString();
        ratingSlider.maxValue = 100;
        ratingSlider.value = rating;

        perfectReviews.text = data.PerfectReviews.ToString();
        goodReviews.text= data.GoodReviews.ToString();
        normalReviews.text = data.NormalReviews.ToString();
        badReviews.text = data.BadReviews.ToString();
    }

    private int CalculateRating(RatingStats stats)
    {
        int result = 0;

        result += stats.PerfectReviews * 2;
        result += stats.GoodReviews;
        result += stats.BadReviews * -3;

        Rating = result;

        return result;
    }
}
