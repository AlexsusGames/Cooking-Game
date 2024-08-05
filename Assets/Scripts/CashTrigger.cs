using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTrigger : MonoBehaviour
{
    private BoxCollider boxCollider;
    private GameObject lastPerson;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Service()
    {
        lastPerson.TryGetComponent(out CharacterMove movement);
        if(movement != null) { movement.OnServiced(false); }
    }

    private void OnTriggerEnter(Collider other)
    {
        lastPerson = other.gameObject;
    }
}
