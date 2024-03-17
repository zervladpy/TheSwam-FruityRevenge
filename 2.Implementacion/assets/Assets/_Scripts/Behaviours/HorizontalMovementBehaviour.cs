using UnityEngine;

public class HorizontalMovementBehaviour : StateMachineBehaviour
{
    private Rigidbody2D Body;
    private CharacterStats Stats;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Body = animator.GetComponent<Rigidbody2D>();
        Stats = animator.GetComponent<Character>().Stats;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        float dirX = animator.GetFloat(PAP.DirX);

        Body.velocity = new Vector2(dirX * Stats.Velocity, Body.velocity.y);

        animator.SetBool(PAP.IsMoving, dirX != 0f);

    }

}
