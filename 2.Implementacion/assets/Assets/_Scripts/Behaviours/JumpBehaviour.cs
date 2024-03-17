using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour
{

    private Rigidbody2D Body;
    private CharacterStats Stats;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(PAP.Jump);

        Debug.Log("Jumping");

        Body = animator.GetComponent<Rigidbody2D>();
        Stats = animator.GetComponent<Character>().Stats;

        Body.velocity = new Vector2(Body.velocity.x, Stats.JumpForce); ;
        
    }

}
