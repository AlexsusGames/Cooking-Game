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
                    var cup = objects[objects.Count - 1];
                    objects.Remove(cup);
                    handler.ChangeObject(cup, true);
                }
                else ShowAdvice("������� ���� ����� �������!");
            }

            else if (obj.TryGetComponent(out Cup _) && !IsMaxCountOfObjects)
            {
                PutObject(obj);
                handler.ChangeObject();
            }
        }
    }
}