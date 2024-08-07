using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    private Transform[] wayPoints;
    private Rigidbody rb;
    private Animator animator;
    private CapsuleCollider trigger;
    private AnimationController animController;

    private int currentIndex;
    private bool isQueue;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        trigger = GetComponent<CapsuleCollider>();

        animController = new(animator);
    }

    public void Bind(Transform[] wayPoints)
    {
        this.wayPoints = wayPoints;
        currentIndex = 0;
        gameObject.SetActive(true);
        animController.SetWalkingState(1);
    }

    public void OnServiced(bool isTriggerable)
    {
        ContinueWalking();
        Invoke(nameof(ColliderEnable), 2f);
    }
    public void ColliderEnable(bool isTriggerable)
    {
        trigger.enabled = isTriggerable;
    }
    public void ColliderEnable()
    {
        trigger.enabled = false;
    }

    private void ResetCharacter()
    {
        currentIndex = 0;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isQueue)
        {
            Vector3 targetPosition = wayPoints[currentIndex].position;
            Vector3 moveDirection = (targetPosition - rb.position).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime));
            }

            if (currentIndex < wayPoints.Length - 1)
            {
                rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

                if (Vector3.Distance(wayPoints[currentIndex].position, rb.position) < 0.1f)
                {
                    currentIndex++;
                }
            }
            else ResetCharacter();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("People"))
        {
            isQueue = true;
            animController.SetWalkingState(0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("People"))
        {
            Invoke(nameof(ContinueWalking), 1f);
        }
    }

    private void ContinueWalking()
    {
        isQueue = false;
        animController.SetWalkingState(1);
    }
}
