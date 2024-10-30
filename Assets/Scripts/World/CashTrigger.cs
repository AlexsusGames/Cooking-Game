using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CashTrigger : InteractiveManager
{
    [SerializeField] private OrderView view;
    [SerializeField] private Bank bank;
    [SerializeField] private float timeToService;
    [SerializeField] private ClientQueue queue;

    private const string QUEST_REQUEST = "serving";
    [Inject] private QuestHandler questHander;
    [Inject] private InteractSound sound;

    private GameObject lastPerson;
    private List<RecipeConfig> randomFood;

    private float cathedTime;
    private event Action<float> Timer;
    private float time;

    public float TimeToService { get => timeToService; set => timeToService = value; }

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
            var fillAmount = time / cathedTime;
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

        if (!string.IsNullOrEmpty(randomRecipe))
        {
            randomFood.Add(foodConfigFinder.GetRecipeByName(randomRecipe));

            if (!randomFood[0].IsDrink)
            {
                var randomDrink = foodConfigFinder.GetRandomSellingDrink();

                if (randomDrink != null)
                {
                    if (UnityEngine.Random.Range(0, 2) > 0)
                        randomFood.Add(randomDrink);
                }
            }
        }
        else randomFood = null;
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
            cathedTime = timeToService;
            time = cathedTime;
        }
        else Service();
    }
    private void OnTriggerExit(Collider other) => randomFood = null;

    public override void Interact()
    {
        var player = GetPlayer();
        var handler = player.GetComponent<ObjectHandler>();
        var obj = handler.GetObject();

        if(lastPerson == null)
        {
            return;
        }

        if (obj == null)
        {
            ShowAdvice("  сожалению данный товар закончилс€");
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
                    questHander.TryChangeProgress(QUEST_REQUEST);

                    TaxCounter.PeopleServed++;

                    sound.Play(NonLoopSounds.Cash);
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
