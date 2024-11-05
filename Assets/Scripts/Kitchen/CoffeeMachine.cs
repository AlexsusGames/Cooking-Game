using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CoffeeMachine : InteractiveManager, IUpgradable
{
    [SerializeField] private ParticleGroup effect;
    [SerializeField] private ProductConfig[] coffeeRecipe;
    [Inject] private CoffeeKeeper coffeeKeeper;
    [Inject] private InteractSound sound;
    [SerializeField] private GameObject spoiltCoffee;
    private FoodConfigFinder foodConfigFinder = new();
    private bool isWorking;
    private int msTime = 5000;
    public InteractivePlaces InteractiveType => InteractivePlaces.CoffeeMachine;
    public int InteractiveTime { get => msTime; set => msTime = 5000 - value * 1000; }
    private string[] advices = { "�� ������� ����� �� �����.", "����-�� �� �������...", "���� ������." };

    public override async void Interact()
    {
        var player = GetPlayer();
        var inventory = player.gameObject.GetComponent<Inventory>();
        var inventoryProducts = foodConfigFinder.GetProductsByName(inventory.GetProducts());
        var handler = player.GetComponent<ObjectHandler>();
        var handleObject = handler.GetObject();

        if (handleObject == null && !isWorking)
        {
            if (CompareRecipes(inventoryProducts))
            {
                if (!coffeeKeeper.IsMaxCountOfObjects)
                {
                    isWorking = true;
                    TaxCounter.Taxes += 0.5f;

                    effect.Play();

                    var model = foodConfigFinder.CookingFood(inventoryProducts, InteractiveType);

                    inventory.RemoveProducts();

                    sound.Play(InteractiveType, msTime);
                    await Task.Delay(msTime);

                    if (model != null)
                    {
                        coffeeKeeper.CreateObject(model.Model);
                    }
                    else coffeeKeeper.CreateObject(spoiltCoffee);

                    effect.Stop();
                    isWorking = false;
                }
                else ShowAdvice(advices[0]);
            }
            else ShowAdvice(advices[1]);
        }
        else ShowAdvice(advices[2]);
    }

    private bool CompareRecipes(List<ProductConfig> recipe)
    {
        if (recipe.Count < coffeeRecipe.Length)
            return false;

        for (int i = 0; i < coffeeRecipe.Length; i++)
        {
            if (!recipe.Contains(coffeeRecipe[i]))
                return false;
        }

        return true;
    }
    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            CachedKeys = advices;
        }

        return CachedKeys;
    }

    public override void Set(params string[] param) => advices = param;
}
