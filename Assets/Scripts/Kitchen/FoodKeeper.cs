using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FoodKeeper : Keeper
{
    [Inject] private InteractSound sound;
    private string[] advices = { };
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
                    sound.Play(NonLoopSounds.Plate);
                }
            }

            else if (obj.TryGetComponent(out Dish _) && !IsMaxCountOfObjects)
            {
                PutObject(obj);
                sound.Play(NonLoopSounds.Plate);
                handler.ChangeObject();
            }
        }
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
