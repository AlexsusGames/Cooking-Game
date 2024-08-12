using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    public void OpenDoor(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
