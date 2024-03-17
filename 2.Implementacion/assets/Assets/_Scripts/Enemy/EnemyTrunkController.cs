using UnityEngine;

public class EnemyTrunkController : MonoBehaviour
{

    [SerializeField] private GameObject Projectile;
    [SerializeField] private LayerMask PlayerMask;

    private const float RAYCAST_SIZE_X = 20f;
    private Animator Animator;

    private EnemyHorizontalController MovementController;
    private CollisionManager collisionManager;

    void Start()
    {
        Animator = GetComponent<Animator>();
        MovementController = GetComponent<EnemyHorizontalController>();
        collisionManager = GetComponent<CollisionManager>();
    }

    void Update()
    {

        bool IsShooting = Animator.GetBool("IsShooting");

        if (FindPlayer())
        {
            if (!IsShooting)
            {
                Animator.SetBool("IsShooting", true);

                MovementController.Stop();

            }
        } else
        {
            MovementController.Resume();
            Animator.SetBool("IsShooting", false);
        }

        if (collisionManager.TopCollision)
        {
            Debug.Log("Top Collision");
            if (collisionManager.Collider.CompareTag("Hero"))
            {
                Animator.SetTrigger("Die");
            }

        }
    }

    /// <summary>
    /// Draws a Straight Ray In Front of the Character To find a PlayerMask
    /// </summary>
    /// <returns>If Ray Hitted a Player SurfaceMask</returns>
    private bool FindPlayer()
    {
        float dirHitVector = Animator.GetFloat("DirX");
        Vector2 dirVector = new Vector2(-dirHitVector, 0f);
        RaycastHit2D lateralHitCheck = Physics2D.Raycast(transform.position, dirVector, RAYCAST_SIZE_X, PlayerMask);

        bool targetted = lateralHitCheck.collider != null;

        return targetted;
    }


    /// <summary>
    /// When the animation is Ready to Shoot is Triggered
    /// </summary>
    public void Shoot()
    {

        bool isFlipped = GetComponent<SpriteRenderer>().flipX;

        Vector3 ProjectileSpawnPos = new Vector3(transform.position.x - (isFlipped ? -1f : 1f), transform.position.y - 0.25f);

        GameObject NewProjectile = Instantiate(Projectile, ProjectileSpawnPos, Quaternion.identity);

        NewProjectile.GetComponent<SpriteRenderer>().flipX = isFlipped;

        NewProjectile.GetComponent<ProjectileController>().SetParams(isFlipped ? 1f: -1f);

    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

}
