using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Partnership : MonoBehaviour
{
    [SerializeField] private DrinkFridgeView[] views;
    [SerializeField] private DrinkFridgeConfig[] configs;

    [SerializeField] private CurrentPartershipView partnershipView;
    [SerializeField] private DrinkFridgePresenter partnershipPresenter;
    [SerializeField] private PartershipRatingTitle rating;

    [Inject] private RatingManager ratingManager;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        UpdateViews();
    }

    private void Init()
    {
        for (int i = 0; i < views.Length; i++)
        {
            var config = configs[i];
            var view = views[i];

            view.Init(config);

            UnityAction action = () =>
            {
                partnershipPresenter.Preview(config, rating.Rating);
            };

            view.AssingListener(action);
        }
    }

    private void UpdateViews()
    {
        var currentPartnership = ratingManager.GetStats().CurrentRatingFridge;

        if(!string.IsNullOrEmpty(currentPartnership))
        {
            var config = GetConfigByName(currentPartnership);
            partnershipView.SetData(config.DrinkType);
        }
    }

    private DrinkFridgeConfig GetConfigByName(string name)
    {
        for (var i = 0; i < configs.Length; i++)
        {
            if (configs[i].name == name)
                return configs[i];
        }

        throw new System.Exception($"There is no config, name - {name}");
    }
}
