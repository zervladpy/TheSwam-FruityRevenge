using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class OnAirBehaviour : StateMachineBehaviour
{

    private Rigidbody2D Body;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Body = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        float velY = Body.velocity.y;

        animator.SetFloat(PAP.VelY, velY);

    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset VelY
        animator.SetFloat(PAP.VelY, 0f);

        // Reset DoubleJump
        animator.SetBool(PAP.DoubleJump, false);

    }


}
