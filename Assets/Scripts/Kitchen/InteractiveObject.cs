using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Outline interactiveMesh;
    [SerializeField] private BoxCollider interactiveZone;
    [SerializeField] private string button;

    [SerializeField] private UnityEvent action;
    private bool isInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        isInTrigger = true;
        if(interactiveMesh != null)
        {
            interactiveMesh.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        if (interactiveMesh != null)
        {
            interactiveMesh.enabled = false;
        }
    }

    private void Update()
    {
        if(isInTrigger)
        {
            if(Input.GetButtonDown(button))
            {
                action?.Invoke();
            }
        }
    }
}
