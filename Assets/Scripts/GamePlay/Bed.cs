using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bed : InteractiveManager
{
    [Inject] private ProgressManager progressManager;
    private string[] advices = { "Рано спать." };
    public override void Interact()
    {
        if (!progressManager.EndDay())
        {
            ShowAdvice(advices[0]);
        }
        else
        {
            var player = GetPlayer();

            player.TryGetComponent(out MoveController controller);
            controller.Interact();
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

