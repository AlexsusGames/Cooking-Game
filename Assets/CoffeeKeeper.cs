using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeKeeper : InteractiveManager
{
    [SerializeField] private GameObject coffeePrefab;
    [SerializeField] private Transform[] parents;
    private List<GameObject> objects = new();
    private int maxDishCount => parents.Length;

    public int CountOfCoffee
    {
        get => objects.Count;
        set
        {
            for (int i = 0; i < value; ++i)
            {
                CreateCoffee();
            }
        }
    }
    public bool IsMaxCountOfDish => objects.Count >= maxDishCount;

    public void CreateCoffee()
    {
        for(int i = 0;i < parents.Length; ++i)
        {
            if (parents[i].childCount == 1)
                continue;

            var dish = Instantiate(coffeePrefab, parents[i]);

            objects.Add(dish);
            return;
        }
    }

    public override void Interact()
    {
        var player = GetPlayer().gameObject;

        if (player.TryGetComponent(out ObjectHandler handler))
        {
            var obj = handler.GetObject();

            if (obj == null)
            {
                if (objects.Count > 0)
                {
                    var cup = objects[objects.Count - 1];
                    objects.Remove(cup);
                    handler.ChangeObject(cup, true);
                }
                else ShowAdvice("Сначала кофе нужно сварить!");
            }

            else if (obj.TryGetComponent(out Cup cup))
            {
                CreateCoffee();
                handler.ChangeObject();
            }
        }
    }
}
