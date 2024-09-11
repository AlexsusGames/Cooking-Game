using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodKeeper : Keeper
{
    private GameObject GetFood()
    {
        var obj = GetOrderedFood();

        if(obj == null)
        {
            obj = objects[objects.Count - 1];
        }

        return obj;
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
                    var dish = GetFood();
                    objects.Remove(dish);
                    handler.ChangeObject(dish);
                }
            }

            else if (obj.TryGetComponent(out Dish _) && !IsMaxCountOfObjects)
            {
                PutObject(obj);
                handler.ChangeObject();
            }
        }
    }
}
