using UnityEngine;


public class RangedEnemyController : EnemyController
{
    [SerializeField] private float Range;
    [SerializeField] private GameObject Projectile;
    [SerializeField] private LayerMask TargetMask;

    private const string DIRX_FLOAT = "DirX";
    private const string IS_SHOOTING = "IsShooting";

  
    new void Update()
    {

        if (FindPlayer())
        {
            Stop();

            if (!Animator.GetBool(IS_SHOOTING))
            {
                Animator.SetBool(IS_SHOOTING, true);
            }

        } else
        {
            Resume();
            if (Animator.GetBool(IS_SHOOTING))
            {
                Animator.SetBool(IS_SHOOTING, false);
            }
        }

        base.Update();
    }


    private bool FindPlayer()
    {
        float dirHitVector = Animator.GetFloat(DIRX_FLOAT);
        Vector2 dirVector = new Vector2(dirHitVector, 0f);
        RaycastHit2D lateralHitCheck = Physics2D.Raycast(transform.position, dirVector, Range, TargetMask);

        return lateralHitCheck.collider != null;

    }

    /// <summary>
    /// Called From Animator, as the projectile is timed with the Animation
    /// </summary>
    private void Shoot()
    {

        bool IsFlipped = GetComponent<SpriteRenderer>().flipX;

        Vector3 ProjectileSpawnPos = new Vector3(transform.position.x - (IsFlipped ? 1f : -1f), transform.position.y - 0.25f);

        GameObject NewProjectile = Instantiate(Projectile, ProjectileSpawnPos, Quaternion.identity);

        NewProjectile.GetComponent<SpriteRenderer>().flipX = IsFlipped;

        NewProjectile.GetComponent<ProjectileController>().SetParams(IsFlipped ? -1f : 1f);

    }

}
