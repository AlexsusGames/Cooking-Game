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

    private string[] advices = { "Стол уже накрыт", "Сюда бы что-нибудь съедобное.." };
    public override void Interact()
    {
        var player = GetPlayer();
        var handler = player.GetComponent<ObjectHandler>();
        var obj = handler.GetObject();

        if(dishPlace.transform.childCount > 0)
        {
            ShowAdvice(advices[0]);
            return;
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
                    handler.EmojiSender.SendEmoji(EmojiType.Heart);
                    return;
                }
            }
        }

        ShowAdvice(advices[1]);
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

    public RecipeConfig GetFood()
    {
        if(dishPlace.transform.childCount > 0)
        {
            dishPlace.GetChild(0).TryGetComponent(out Dish dish);
            return dish.GetFood();
        }

        return null;
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
