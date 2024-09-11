using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Keeper : InteractiveManager
{
    [SerializeField] private GameObject spoiltProduct;
    [SerializeField] private Transform[] parents;
    [SerializeField] private CashTrigger cash;
    protected List<GameObject> objects = new();
    protected int MaxObjectCount => parents.Length;

    public int CountOfFood
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

        product.transform.localPosition = Vector3.zero;
        product.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    protected Transform GetFreePosition()
    {
        for (int i = 0; i < parents.Length; i++)
        {
            if (parents[i].transform.childCount == 0)
            {
                return parents[i].transform;
            }
        }

        return null;
    }

    public GameObject GetOrderedFood()
    {
        var order = cash.GetOrder();

        if(order != null)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].TryGetComponent(out IFood food))
                {
                    if (order.Contains(food.GetFood()))
                    {
                        return objects[i];
                    }
                }
            }
        }
        return null;
    }
}
