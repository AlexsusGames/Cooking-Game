using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Keeper : InteractiveManager
{
    [SerializeField] private GameObject spoiltProduct;
    [SerializeField] private Transform[] parents;
    protected List<GameObject> objects = new();
    protected int MaxObjectCount => parents.Length;

    public int CountOfCoffee
    {
        get => objects.Count;
        set
        {
            for (int i = 0; i < value; ++i)
            {
                CreateObject(spoiltProduct);
            }
        }
    }
    public bool IsMaxCountOfObjects => objects.Count >= MaxObjectCount;

    public void CreateObject(GameObject product)
    {
        for (int i = 0; i < parents.Length; ++i)
        {
            if (parents[i].childCount == 1)
                continue;

            var obj = Instantiate(product, parents[i]);

            objects.Add(obj);
            return;
        }
    }

    public void PutObject(GameObject product)
    {
        var parent = GetFreePosition();

        product.transform.SetParent(parent);
        objects.Add(product);

    }

    protected Transform GetFreePosition()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].transform.childCount == 0)
            {
                return objects[i].transform;
            }
        }

        return null;
    }
}
