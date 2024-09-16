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
    private List<RecipeConfig> randomFood;

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
        randomFood = new();
        string randomRecipe = knownRecipes.GetRandomSellingRecipe();
        randomFood.Add(foodConfigFinder.GetRecipeByName(randomRecipe));

        if (!randomFood[0].IsDrink)
        {
            var randomDrink = foodConfigFinder.GetRandomSellingDrink();

            if(randomDrink != null)
            {
                randomFood.Add(randomDrink);
            }
        }
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
            view.ShowClientOrder(randomFood);
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
            view.UpdateView(null);
            Service();
            return;
        }

        obj.TryGetComponent(out IFood food);

        if (food != null && randomFood != null)
        {
            var config = food.GetFood();

            if (randomFood.Contains(config))
            {
                randomFood.Remove(config);
                handler.GetRidOfLastObject();
                view.UpdateView(randomFood);
                bank.Change(config.Price);
                TaxCounter.Income += config.Price;

                if (randomFood.Count == 0)
                {
                    TaxCounter.PeopleServed++;

                    ShowAdvice("ѕриходите еще!");

                    Service();
                }
                else
                {
                    time += timeToService / 2;
                    time = time > timeToService ? timeToService : time;
                }
            }
            else ShowAdvice("Ёто блюдо не заказывали..");
        }
    }
    public List<RecipeConfig> GetOrder()
    {
        if(randomFood != null)
        {
            return randomFood;
        }

        return null;
    }
}
