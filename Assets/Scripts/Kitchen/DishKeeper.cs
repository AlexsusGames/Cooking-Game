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

    private void Awake() => CreatePool();
    private void CreatePool()
    {
        for (int i = 0; i < maxDishCount; i++)
        {
            CreateDish();
        }   
    }

    private void CreateDish()
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
            }

            else if (obj.TryGetComponent(out Dish _))
            {
                CreateDish();
                handler.ChangeObject();
            }
        }
    }
}
