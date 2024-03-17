using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBehaviour : StateMachineBehaviour
{
    private float dirX;
    private SpriteRenderer spriteRenderer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        dirX = animator.GetFloat(PAP.DirX);

        if (dirX > 0f)
        {
            spriteRenderer.flipX = false;
        }
        else if (dirX < 0f)
        {
            spriteRenderer.flipX = true;
        }

        if (dirX != 0f)
        {
            animator.SetInteger(PAP.LastDirX, (int)dirX);

        }
    }
}
