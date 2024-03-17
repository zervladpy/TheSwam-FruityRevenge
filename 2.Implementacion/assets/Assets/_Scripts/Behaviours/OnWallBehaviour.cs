using UnityEngine;

public class OnWallBehaviour : StateMachineBehaviour
{

    private Rigidbody2D Body;
    private CharacterStats Stats;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Body = animator.GetComponent<Rigidbody2D>();
        Stats = animator.GetComponent<Character>().Stats;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Body.velocity = new Vector2(Body.velocity.x, Mathf.Clamp(Body.velocity.y, Stats.WallSpeed, float.MaxValue));
    }

}
