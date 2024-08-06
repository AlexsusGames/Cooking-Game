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

    public void SetWalkingState(int value)
    {
        animator.SetInteger("walkingState", value);
    }
}
