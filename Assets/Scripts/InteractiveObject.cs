using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private BoxCollider interactiveZone;
    [SerializeField] private string button;

    [SerializeField] private UnityEvent action;
    private bool isInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        isInTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
    }

    private void Update()
    {
        if(isInTrigger)
        {
            if(Input.GetButtonDown(button))
            {
                action?.Invoke();
                Debug.Log("Button " + button + " works");
            }
        }
    }
}
