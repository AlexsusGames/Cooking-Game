using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMove : MonoBehaviour
{
    private Transform[] wayPoints;
    private Animator animator;
    private AnimationController animController;
    private NavMeshAgent agent;

    private int currentIndex;
    private bool isQueue;

    private Vector3 queuePosition;
    private Vector3 starePoint;

    public bool IsServed { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        animController = new(animator);
    }

    private float GetPathDistance(NavMeshPath path)
    {
        float distance = 0f;

        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }

    public float GetDistanceFromQueue(Transform aim)
    {
        NavMeshPath path = new();

        if(agent.CalculatePath(aim.position, path))
        {
            return GetPathDistance(path);
        }
        return 100;
    }

    public void Bind(Transform[] wayPoints)
    {
        IsServed = false;
        this.wayPoints = wayPoints;
        currentIndex = 0;
        gameObject.SetActive(true);
        animController.SetWalkingState(WalkingStates.Walking);
    }

    private void ResetCharacter()
    {
        currentIndex = 0;
        gameObject.SetActive(false);
    }

    public void GoToQueuePoint(Vector3 position, Vector3 starePoint)
    {
        IsServed = true;
        isQueue = true;
        queuePosition = position;
        this.starePoint = starePoint;

        Debug.Log(name + " is going to queue");
    }

    private void Update()
    {
        if (agent.velocity != Vector3.zero)
        {
            animController.SetWalkingState(WalkingStates.Walking);
        }
        else animController.SetWalkingState(WalkingStates.Idle);

        if (!isQueue)
        {
            Vector3 targetPosition = wayPoints[currentIndex].position;

            if (currentIndex < wayPoints.Length - 1)
            {
                agent.SetDestination(targetPosition);


                if (Vector3.Distance(targetPosition, transform.position) < 0.1f)
                {
                    currentIndex++;
                }
            }
            else ResetCharacter();
        }
        else
        {
            if(queuePosition != Vector3.zero)
            {
                agent.SetDestination(queuePosition);

                if (Vector3.Distance(queuePosition, transform.position) < 0.1f)
                { 
                    transform.LookAt(starePoint);
                }
            }
        }
    }

    public void ContinueWalking()
    {
        isQueue = false;
        ServeDelay();
    }

    public async void ServeDelay()
    {
        await Task.Delay(10000);

        IsServed = false;
    }
}
