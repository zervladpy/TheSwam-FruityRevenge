using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : Character
{
    [SerializeField] private LayerMask SurfaceMask;    
        
    private const string PLAYER_TAG = "Hero";
    private const string DIRX_FLOAT = "DirX"; 
    private const string LAST_DIRX_INTEGER = "LastDirX"; 
    private const string IS_MOVING_BOOL = "IsMoving"; 
    
    private Collider2D Collider;
    private Rigidbody2D Body;


    new public void Start()
    {
        base.Start();
        Collider = GetComponent<Collider2D>();
        Body = GetComponent<Rigidbody2D>();

        Animator.SetFloat(DIRX_FLOAT, Random.Range(0, 2) == 0 ? -1 : 1);
        Animator.SetInteger(LAST_DIRX_INTEGER, Random.Range(0, 2) == 0 ? -1 : 1);

    }

    public override void OnAir()
    {
        return;
    }

    public override void Move()
    {
        ChangeDir();


        Vector2 dirForce = new Vector2(DirX * Stats.Velocity, 0f);

        Animator.SetBool(IS_MOVING_BOOL, dirForce != Vector2.zero);

        Body.velocity = dirForce;

    }

    private void ChangeDir()
    {

        Vector2 dirHitVector = new Vector2(DirX, 0f);

        Vector2 surfaceVector = new Vector2(DirX, -1f);

        RaycastHit2D lateralHitCheck = Physics2D.Raycast(transform.position, dirHitVector, 1f, SurfaceMask);
        RaycastHit2D surfaceHitCheck = Physics2D.Raycast(transform.position, surfaceVector, 2f, SurfaceMask);

        Debug.DrawRay(Collider.bounds.center, dirHitVector);
        Debug.DrawRay(Collider.bounds.center, surfaceVector);


        if (lateralHitCheck.collider != null && lateralHitCheck.collider.CompareTag("Surface"))
        {
            Animator.SetFloat(DIRX_FLOAT, DirX * -1);
        }

        if (surfaceHitCheck.collider == null)
        {
            Animator.SetFloat(DIRX_FLOAT, DirX * -1);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag(PLAYER_TAG))
        {
            if (CollisionManager.TopCollision)
            {
                ApplyDamage();
            }
        }

    }

}
