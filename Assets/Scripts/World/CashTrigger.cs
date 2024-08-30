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

    private event Action<float> timer;
    private float time;

    private FoodConfigFinder foodConfigFinder = new();
    private KnownRecipes knownRecipes = new();

    private void Awake()
    {
        timer += view.ChangePatience;
    }

    private void FixedUpdate()
    {
        if(time > 0)
        {
            time -= Time.fixedDeltaTime;
            var fillAmount = time / timeToService;
            timer?.Invoke(fillAmount);
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
        queue.Service();
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeRecipe();
        time = timeToService;
        lastPerson = other.gameObject;

        view.ShowClientOrder(randomFood.picture);
    }

    public override void Interact()
    {
        var player = GetPlayer();
        var handler = player.GetComponent<ObjectHandler>();
        handler.GetObject().TryGetComponent(out Dish dish);

        if(dish != null && randomFood != null)
        {
            if (dish.GetFood() == randomFood)
            {
                handler.GetRidOfObject();
                bank.Change(randomFood.Price);
                time = 0.02f;
                Service();
            }
        }
    }
}
