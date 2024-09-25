using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class GirlController : MonoBehaviour
{
    [SerializeField] private GameObject bag;
    private NavMeshAgent agent;
    private Animator animator;
    private string catchedAction;

    private CancellationTokenSource cancellationTokenSource;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void ChangeBagEnabled()
    {
        if (bag.activeInHierarchy) bag.SetActive(false);
        else bag.SetActive(true);
    }

    public async Task Commit(NpcActionCommand command)
    {
        cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;

        var rotation = command.TargetRotation;
        if (!string.IsNullOrEmpty(catchedAction))
        {
            animator.SetBool(catchedAction, false);
        }

        agent.enabled = true;
        catchedAction = command.animatorAction;
        agent.SetDestination(command.Target.position);

        await Task.Delay(100); // agent bag

        try
        {
            while (!token.IsCancellationRequested)
            {
                while (agent.remainingDistance > command.distanceOffset)
                {
                    int walkingState = agent.velocity.sqrMagnitude > 0 ? 1 : 0;
                    animator.SetInteger("walkingState", walkingState);
                    await Task.Delay(100, token);
                }

                await Task.Delay(command.MsDelay, token);

                if (agent.remainingDistance <= command.distanceOffset)
                    break;

                await Task.Yield();
            }
        }
        catch(OperationCanceledException)
        {
            animator.SetInteger("walkingState", 0);
        }

        agent.isStopped = true;
        agent.enabled = false;

        if (!string.IsNullOrEmpty(command.animatorAction))
        {
            animator.SetBool(command.animatorAction, true);

        }
        else animator.SetInteger("walkingState", 0);

        transform.position = command.Target.position;

        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.localRotation = targetRotation;
    }
    public void CancelToken()
    {
        if(cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
