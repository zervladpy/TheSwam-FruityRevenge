using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{

    private CollisionManager collisionManager;
    private Animator animator;

    void Start()
    {
        collisionManager = GetComponent<CollisionManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        bool lateralCollision = collisionManager.LeftCollision || collisionManager.RightCollision;
        bool wasWalled = animator.GetBool(PAP.WallCollision);

        if (!wasWalled) { 
            animator.SetTrigger(PAP.WallSliding);           
        }

        animator.SetBool(PAP.WallCollision, lateralCollision);

        bool bottomCollision = collisionManager.BottomCollision;

        animator.SetBool(PAP.OnGround, bottomCollision);
        animator.SetBool(PAP.OnAir, !bottomCollision);

        Collider2D collision = collisionManager.Collider;

        if (collision == null) { return; }

        if (collision.CompareTag("Spikes") || collision.CompareTag("Projectile")) {
            
            if (!animator.GetBool(PAP.IsDying)) {
                animator.SetBool(PAP.IsDying, true);
                animator.SetTrigger(PAP.Die);

                GameManager.Instance.ResetScene();
            }

        }

    }

    public void ApplyDamage()
    {

    }

}
