using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodKeeper : Keeper
{
    private GameObject GetFood()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            return objects[i];
        }

        return null;
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
