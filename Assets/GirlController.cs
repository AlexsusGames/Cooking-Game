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
    private GirlAnimationState state;
    private string catchedAction;

    private Vector3 cachedPosition;
    private CancellationTokenSource cancellationTokenSource;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        state = GetComponent<GirlAnimationState>();
    }

    public async Task Commit(NpcActionCommand command)
    {
        cancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = cancellationTokenSource.Token;
        state.ResetActions();

        if (cachedPosition != Vector3.zero)
        {
            transform.position = cachedPosition;
        }

        var rotation = command.TargetRotation;
        if (!string.IsNullOrEmpty(catchedAction))
        {
            animator.SetBool(catchedAction, false);
        }

        agent.enabled = true;
        catchedAction = command.mainAction;
        agent.SetDestination(command.Target.position);

        await Task.Delay(100); // agent bag

        try
        {
            while (agent.isOnNavMesh && agent.remainingDistance > command.distanceOffset)
            {
                int walkingState = agent.velocity.sqrMagnitude > 0 ? 1 : 0;
                animator.SetInteger("walkingState", walkingState);
                await Task.Delay(100, token);
            }

            if (!token.IsCancellationRequested)
            {
                cachedPosition = transform.position;

                agent.isStopped = true;

                await Task.Yield();

                agent.enabled = false;

                if (!string.IsNullOrEmpty(command.mainAction))
                {
                    animator.SetBool(command.mainAction, true);
                }
                else animator.SetInteger("walkingState", 0);

                state.CommitAction(command.additionalAction);

                transform.position = command.Target.position;

                Quaternion targetRotation = Quaternion.Euler(rotation);
                transform.localRotation = targetRotation;

                await Task.Delay(command.MsDelay, token);
            }
        }
        catch(OperationCanceledException)
        {
            animator.SetInteger("walkingState", 0);
            cachedPosition = Vector3.zero;
        }
    }
    public void CancelToken()
    {
        if(cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
