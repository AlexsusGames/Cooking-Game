using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedTable : InteractiveManager
{
    [SerializeField] private Transform dishPlace;
    public override void Interact()
    {
        var player = GetPlayer();
        var handler = player.GetComponent<ObjectHandler>();
        var obj = handler.GetObject();

        if(dishPlace.transform.childCount > 0)
        {
            ShowAdvice("Стол уже накрыт");
        }

        if( obj != null)
        {
            if(obj.TryGetComponent(out Dish dish))
            {
                var food = dish.GetFood();

                if(food != null && !food.IsDrink)
                {
                    obj.transform.parent = dishPlace;
                    obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
                    handler.ChangeObject();
                    return;
                }
            }
        }

        ShowAdvice("Сюда бы что-нибудь съедобное..");
    }

    public bool IsCovered()
    {
        return dishPlace.transform.childCount > 0;
    }

    public void RemoveFood()
    {
        Destroy(dishPlace.GetChild(0).gameObject);
    }
}
