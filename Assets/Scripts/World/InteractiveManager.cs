using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractiveManager : MonoBehaviour
{
    [SerializeField] private float radius = 20f;

    protected MoveController GetPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out MoveController player))
            {
                return player;
            }
        }
        throw new System.Exception("Player isn't found");
    }

    protected void ShowAdvice(string text)
    {
        var player = GetPlayer();
        player.TryGetComponent(out AdviceLine line);
        line.ShowAdvice(text);
    }

    public abstract void Interact();
}
