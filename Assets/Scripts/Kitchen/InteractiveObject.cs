using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class InteractiveObject : MonoLocalization
{
    [SerializeField] private Outline interactiveMesh;
    [SerializeField] private BoxCollider interactiveZone;
    [SerializeField] private string button;
    [Header("InputView")]
    [Inject] private InputView inputView;
    [SerializeField] private string actionDescribtion;

    [SerializeField] private UnityEvent action;
    private bool isInTrigger;

    public override string[] Get()
    {
        if (CachedKeys == null)
            CachedKeys = new string[] { actionDescribtion };

        return CachedKeys;
    }

    public override void Set(params string[] param) => actionDescribtion = param[0];

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
