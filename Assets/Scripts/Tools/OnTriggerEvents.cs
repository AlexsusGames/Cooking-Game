using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent OnEnter;
    [SerializeField] private UnityEvent OnExit;

    private void OnTriggerEnter(Collider other) => OnEnter?.Invoke();
    private void OnTriggerExit(Collider other) => OnExit?.Invoke();
}
