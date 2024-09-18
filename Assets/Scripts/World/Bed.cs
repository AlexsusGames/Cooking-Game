using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : InteractiveManager
{
    [SerializeField] private ProgressManager progressManager;
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
