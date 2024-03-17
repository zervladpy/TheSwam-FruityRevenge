using UnityEngine;

public class DieBehaviour : StateMachineBehaviour
{

    private float JUMP_FORCE_MULTIPLIER = 3.5f;
    private float SPEED_FORCE_MULTIPLIER = 1.5f;

    private Rigidbody2D Body;
    private CharacterStats Stats;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Stats = animator.GetComponent<Character>().Stats;

        Body = animator.GetComponent<Rigidbody2D>();

        int lastDir = animator.GetInteger(PAP.LastDirX);

        Body.AddForce(new Vector2(-lastDir * Stats.Velocity * SPEED_FORCE_MULTIPLIER, Stats.JumpForce * JUMP_FORCE_MULTIPLIER), ForceMode2D.Impulse);        
    }

}
