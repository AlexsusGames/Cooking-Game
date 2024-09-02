using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DishKeeper : InteractiveManager
{
    [SerializeField] private GameObject dishPrefab;
    [SerializeField] private Transform[] parents;
    private List<GameObject> dishes = new();
    private int maxDishCount = 22;

    public int CountOfDish
    {
        get => dishes.Count;
        set
        {
            for (int i = 0; i < value; ++i)
            {
                CreateDish();
            }
        }
    }
    public bool IsMaxCountOfDish => dishes.Count >= maxDishCount;

    public void CreateDish()
    {
        var number = Random.Range(0, parents.Length);
        var dish = Instantiate(dishPrefab, parents[number]);

        float offset = (float)parents[number].childCount / 10;
        dish.transform.localPosition = new Vector3(0, offset, 0);
        dishes.Add(dish);
    }

    public override void Interact()
    {
        var player = GetPlayer().gameObject;

        if (player.TryGetComponent(out ObjectHandler handler))
        {
            var obj = handler.GetObject();

            if (obj == null)
            {
                if (dishes.Count > 0)
                {
                    var dish = dishes[dishes.Count - 1];
                    dishes.Remove(dish);
                    handler.ChangeObject(dish);
                }
                else ShowAdvice("Чистых тарелок нет..\nпридется мыть.");
            }

            else if (obj.TryGetComponent(out Dish dish))
            {
                if(dish.GetFood() == null)
                {
                    CreateDish();
                    handler.ChangeObject();
                }
            }
        }
    }
}
