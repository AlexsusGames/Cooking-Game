using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTrigger : MonoBehaviour
{
    private GameObject lastPerson;

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
