using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAnimationState : MonoBehaviour
{
    [SerializeField] private GameObject bag;
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject spoon;

    private Animator animator;
    private GameObject cachedObj;
    private string catchedAction;

    private void Awake() => animator = GetComponent<Animator>();

    private void StartReading(string param)
    {
        StartActing(param);
        book.SetActive(true);
        cachedObj = book;
    }

    private void StartEating(string param)
    {
        StartActing(param);
        spoon.SetActive(true);
        cachedObj = spoon;
    }

    private void StartActing(string action)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetBool(action, true);
    }
    public void ChangeBagEnabled()
    {
        if (bag.activeInHierarchy) bag.SetActive(false);
        else bag.SetActive(true);
    }

    public void CommitAction(string action)
    {
        catchedAction = action;
        switch (action)
        {
            case "reading": StartReading(action); break;
            case "eating": StartEating(action); break;
            default:
                {
                    if (!string.IsNullOrEmpty(action))
                    {
                        StartActing(action);
                    }
                    break;
                }
        }
    }

    public void ResetActions()
    {
        animator.SetLayerWeight(1, 0);

        if(!string.IsNullOrEmpty(catchedAction))
        {
            animator.SetBool(catchedAction, false);
        }

        if(cachedObj != null)
        {
            cachedObj.SetActive(false);
        }
    }
}
