using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Stamina stamina;
    [SerializeField] private Transform playerRotation;

    private Animator animator;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isInteract;

    private AnimationController anims;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anims = new(animator);
    }


    public void Init(Animator animator)
    {
        this.animator = animator;
        anims.ChangeController(animator);
    }

    private void Update()
    {
        if(!isInteract)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            if (moveHorizontal == 0 && moveVertical == 0)
            {
                anims.SetWalkingState(WalkingStates.Idle);
                ResetVelosity();
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift) && !stamina.IsExhausted)
                {
                    anims.SetWalkingState(WalkingStates.Running);
                    stamina.Sprint();
                    moveSpeed = 25;
                }
                else
                {
                    anims.SetWalkingState(WalkingStates.Walking);
                    moveSpeed = 15;
                }
            }

            moveDirection = new Vector3(-moveHorizontal, 0.0f, -moveVertical).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                playerRotation.rotation = Quaternion.Slerp(playerRotation.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }

            rb.velocity = moveDirection * moveSpeed;
        }
    }

    private void ResetVelosity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Interact()
    {
        isInteract = true;
        ResetVelosity();
        anims.SetWalkingState(WalkingStates.Idle);
        animator.SetLayerWeight(2, 1);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.right);
        playerRotation.rotation = targetRotation;
    }
    public void FinishInteracting()
    {
        animator.SetLayerWeight(2, 0);
        isInteract = false;
    }
}
