using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Sink : InteractiveManager
{
    private const int INTERRUCT_TIME_MS = 1000;

    [SerializeField] private DishKeeper dishes;
    [SerializeField] private ParticleGroup effect;

    private const string QUEST_REQUEST = "tutor4";
    [Inject] private QuestHandler questHander;
    [Inject] private InteractSound sound;
    private bool isWashing;

    private string[] advices = { "��� ������� ������!" };
    public override void Interact()
    {
        if (!isWashing)
        {
            if (!dishes.IsMaxCountOfDish)
            {
                questHander.TryChangeProgress(QUEST_REQUEST);

               TaxCounter.Taxes += 0.2f;
                var player = GetPlayer();
                player.Interact();

                isWashing = true;
                effect.Play();

                Action action = () => player.FinishInteracting();
                StartCoroutine(Washing(action));
            }
            else ShowAdvice(advices[0]);
        }
    }

    public async void NPC_Interact()
    {
        effect.Play();
        int dishToWash = dishes.MaxDishCount;

        for (int i = 0; i < dishToWash; i++)
        {
            if(dishes.CountOfDish < dishes.MaxDishCount)
            {
                dishes.CreateDish();
            }

            sound.Play(InteractivePlaces.WashStand, INTERRUCT_TIME_MS);
            await Task.Delay(INTERRUCT_TIME_MS);
        } 

        effect.Stop();
    }

    private IEnumerator Washing(Action callBack)
    {
        sound.Play(InteractivePlaces.WashStand, INTERRUCT_TIME_MS);
        yield return new WaitForSeconds(1);
        dishes.CreateDish();
        effect.Stop();
        isWashing = false;
        callBack?.Invoke();
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
