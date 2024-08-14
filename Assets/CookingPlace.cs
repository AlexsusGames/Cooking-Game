using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CookingPlace : InteractiveManager
{
    [SerializeField] private InteractivePlaces place;
    [SerializeField] private ParticleGroup effect;

    private void Awake()
    {
        effect.Stop();
    }

    public override async void Interact()
    {
        effect.Play();
        var player = GetPlayer();

        player.Interact();

        await Task.Delay(5000);

        FinishInterating(player);
    }

    private void FinishInterating(MoveController player)
    {
        player.FinishInteracting();
        effect.Stop();
    }
}
public enum InteractivePlaces
{
    SlicingTable,
    Stove,
    WashStand
}