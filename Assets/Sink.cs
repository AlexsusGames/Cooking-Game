using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : InteractiveManager
{
    [SerializeField] private DishKeeper dishes;
    [SerializeField] private ParticleGroup effect;
    private bool isWashing;
    public override void Interact()
    {
        if (!isWashing && !dishes.IsMaxCountOfDish)
        {
            var player = GetPlayer();
            player.Interact();

            isWashing = true;
            effect.Play();

            Action action = () => player.FinishInteracting();
            StartCoroutine(Washing(action));
        }
    }

    private IEnumerator Washing(Action callBack)
    {
        yield return new WaitForSeconds(1);
        dishes.CreateDish();
        effect.Stop();
        isWashing = false;
        callBack?.Invoke();
    }


}