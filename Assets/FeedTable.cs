using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FeedTable : InteractiveManager
{
    [SerializeField] private Transform dishPlace;

    private const string QUEST_REQUEST = "tutor6";
    [Inject] private QuestHandler questHander;
    [Inject] private FamilyStateManager familyStateManager;
    [Inject] private InteractSound sound;
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
                    questHander.TryChangeProgress(QUEST_REQUEST);

                    sound.Play(NonLoopSounds.Plate);
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
        familyStateManager.IsFed = true;
        Destroy(dishPlace.GetChild(0).gameObject);
    }
}
