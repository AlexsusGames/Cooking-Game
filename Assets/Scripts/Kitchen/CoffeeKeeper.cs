using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeKeeper : Keeper
{
    private string[] advices = { "Сначала кофе нужно сварить!" };
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
                    var orderedCup = GetOrderedFood();

                    if (orderedCup == null)
                    {
                        orderedCup = objects[objects.Count - 1];
                    }

                    objects.Remove(orderedCup);
                    handler.ChangeObject(orderedCup, true);
                }
                else ShowAdvice(advices[0]);
            }

            else if (obj.TryGetComponent(out Cup _) && !IsMaxCountOfObjects)
            {
                PutObject(obj);
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
