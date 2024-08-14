using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private Animator animator;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isInCollision;
    private bool isInteract;

    private AnimationController anims;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anims = new(animator);
    }


    private void Update()
    {
        if(!isInteract)
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            if (moveHorizontal == 0 && moveVertical == 0 || isInCollision)
            {
                anims.SetWalkingState(0);
                ResetVelosity();
                moveSpeed = 1;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anims.SetWalkingState(2);
                    moveSpeed = 25;
                }
                else
                {
                    anims.SetWalkingState(1);
                    moveSpeed = 15;
                }
            }

            moveDirection = new Vector3(-moveHorizontal, 0.0f, -moveVertical).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, turnSpeed * Time.deltaTime));
            }

            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isInCollision = true;
        ContactPoint contact = collision.contacts[0];

        Vector3 collisionDirection = (transform.position - contact.point).normalized;
        rb.AddForce(collisionDirection * 15, ForceMode.Impulse);
    }
    private void OnCollisionExit(Collision collision)
    {
        isInCollision = false;
        ResetVelosity();
    }
    private void ResetVelosity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void Interact()
    {
        isInteract = true;
        animator.SetLayerWeight(2, 1);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.right);
        rb.rotation = targetRotation;
    }
    public void FinishInteracting()
    {
        animator.SetLayerWeight(2, 0);
        isInteract = false;
    }
}
