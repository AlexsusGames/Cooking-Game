using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCInteractor : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out GirlController _))
        {
            action?.Invoke();
        }
    }
}
