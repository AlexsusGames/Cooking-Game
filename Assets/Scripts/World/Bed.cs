using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bed : InteractiveManager
{
    [Inject] private ProgressManager progressManager;
    public override void Interact()
    {
        if (!progressManager.EndDay())
        {
            ShowAdvice("Рано спать.");
        }
        else
        {
            var player = GetPlayer();

            player.TryGetComponent(out MoveController controller);
            controller.Interact();
        }
    }
}
