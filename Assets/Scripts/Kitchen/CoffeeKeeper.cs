using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeKeeper : Keeper
{
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
                else ShowAdvice("Сначала кофе нужно сварить!");
            }

            else if (obj.TryGetComponent(out Cup _) && !IsMaxCountOfObjects)
            {
                PutObject(obj);
                handler.ChangeObject();
            }
        }
    }
}
