using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Outline interactiveMesh;
    [SerializeField] private BoxCollider interactiveZone;
    [SerializeField] private string button;
    [Header("InputView")]
    [Inject] private InputView inputView;
    [SerializeField] private string actionDescribtion;

    [SerializeField] private UnityEvent action;
    private bool isInTrigger;

    private void OnTriggerEnter(Collider other)
    {
        isInTrigger = true;
        if (interactiveMesh != null)
        {
            interactiveMesh.enabled = true;
        }

        if (inputView != null)
        {
            inputView.Show(button, actionDescribtion);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        if (interactiveMesh != null)
        {
            interactiveMesh.enabled = false;
        }

        if (inputView != null)
        {
            inputView.Hide();
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
