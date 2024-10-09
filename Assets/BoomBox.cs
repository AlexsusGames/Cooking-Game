using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BoomBox : MonoBehaviour
{
    [SerializeField] private float standartTimeToServe;
    [SerializeField] private float improvedTimeToServe;
    [Inject] private CashTrigger cash;
    private AudioSource audioSource;
    private Animator animator;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void TurnBoomBox()
    {
        audioSource.enabled = !audioSource.enabled;
        animator.enabled = audioSource.enabled;
        cash.TimeToService = audioSource.enabled ? improvedTimeToServe : standartTimeToServe;
    }
}
