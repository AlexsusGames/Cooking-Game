using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class DrinkFridge : InteractiveManager
{
    [SerializeField] private RecipeConfig[] drinkConfigs;
    [SerializeField] private DrinkType drinkType;

    [Inject] private RatingManager ratingManager;
    private RecipeConfig currentDrink => drinkConfigs[(int)drinkType];

    private string[] clues = { "Рука занята.","Нужно заплатить поставщику." };

    public static RecipeConfig CurrentDrink = null;

    private void Start() => CurrentDrink = currentDrink;

    public override void Interact()
    {
        if (ratingManager.IsMaxDebt())
        {
            ShowAdvice(clues[1]);
            return;
        }

        var player = GetPlayer();
        var objHandler = player.GetComponent<ObjectHandler>();
        var obj = objHandler.GetObject();

        if (obj == null)
        {
            var model = currentDrink.Model;
            var drink = Instantiate(model);

            objHandler.ChangeObject(drink, true);
            ratingManager.RiseDebt(drinkType, true);
        }
        else if(obj.TryGetComponent(out IFood food) && food.GetFood() == currentDrink)
        {
            Destroy(obj.gameObject);
            objHandler.ChangeObject();
            ratingManager.RiseDebt(drinkType, false);
        }
        else ShowAdvice(clues[0]);
    }

    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            CachedKeys = clues;
        }

        return CachedKeys;
    }

    public override void Set(params string[] param) => clues = param;
}
public enum DrinkType
{
    Bubble = 0,
    CitrusBoom = 1,
    FizzUp = 2
}
