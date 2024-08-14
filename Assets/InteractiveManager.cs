using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractiveManager : MonoBehaviour
{
    private float radius = 10f;

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
    public abstract void Interact();
}
