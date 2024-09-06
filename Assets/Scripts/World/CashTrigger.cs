using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTrigger : InteractiveManager
{
    [SerializeField] private OrderView view;
    [SerializeField] private Bank bank;
    [SerializeField] private float timeToService;
    [SerializeField] private ClientQueue queue;
    private GameObject lastPerson;
    private RecipeConfig randomFood;

    private event Action<float> Timer;
    private float time;

    private readonly FoodConfigFinder foodConfigFinder = new();
    private readonly KnownRecipes knownRecipes = new();

    private void Awake()
    {
        Timer += view.ChangePatience;
    }

    private void FixedUpdate()
    {
        if(time > 0)
        {
            time -= Time.fixedDeltaTime;
            var fillAmount = time / timeToService;
            Timer?.Invoke(fillAmount);
        }

        if(lastPerson != null && time <= 0)
        {
            Service();
        }
    }

    private void ChangeRecipe()
    {
        string randomRecipe = knownRecipes.GetRandomSellingRecipe();
        randomFood = foodConfigFinder.GetRecipeByName(randomRecipe);
    }

    private void Service()
    {
        lastPerson.TryGetComponent(out CharacterMove movement);
        if(movement != null) movement.ContinueWalking();
        lastPerson = null;
        queue.Service(movement);
        time = 0.02f;
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeRecipe();
        lastPerson = other.gameObject;

        if (randomFood != null)
        {
            view.ShowClientOrder(randomFood.picture);
            time = timeToService;
        }
        else Service();
    }
    private void OnTriggerExit(Collider other) => randomFood = null;

    public override void Interact()
    {
        var player = GetPlayer();
        var handler = player.GetComponent<ObjectHandler>();
        var obj = handler.GetObject();

        if (obj == null)
        {
            Service();
            return;
        }

        obj.TryGetComponent(out Dish dish);

        if (dish != null && randomFood != null)
        {
            if (dish.GetFood() == randomFood)
            {
                float tax = (float)randomFood.Price / 100;
                TaxCounter.OnServed(tax);

                ShowAdvice("ѕриходите еще!");
                handler.ChangeObject();
                bank.Change(randomFood.Price);
                Service();
            }
            else ShowAdvice("Ёто блюдо не заказывали..");
        }
    }
}
