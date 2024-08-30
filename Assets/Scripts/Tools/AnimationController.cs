using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController 
{
    private Animator animator;
    public AnimationController(Animator animator)
    {
        this.animator = animator;
    }

    public void SetWalkingState(WalkingStates state)
    {
        animator.SetInteger("walkingState", (int)state);
    }
}
public enum WalkingStates
{
    Idle = 0,
    Walking = 1,
    Running = 2
}
