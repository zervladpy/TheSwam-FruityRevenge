using UnityEngine;

public class EnemyHorizontalController : MonoBehaviour
{

    [SerializeField] public float movementSpeed;
    [SerializeField]  LayerMask layerMaskToCollideCheck;

    private int dir;
    private Animator animator;
    private Collider2D coll;

    private bool changedDir;

    private bool IsMoving;

    private void Start()
    {
        IsMoving = true;

        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        dir = Random.Range(0, 2) == 0 ? -1 : 1;

    }

    private void Update()
    {

        if (IsMoving)
        {
            HorizontalMovement();
        }
    }

    private void FixedUpdate()
    {
        ChangeDir();
    }


    private void HorizontalMovement()
    {
        Vector2 dirForce = new Vector2(dir * movementSpeed * Time.deltaTime, 0f);

        animator.SetBool("IsMoving", dirForce != Vector2.zero);

        transform.Translate(dirForce);
    }

    private void ChangeDir()
    {

        Vector2 dirHitVector = new Vector2(dir, 0f);

        Vector2 surfaceVector = new Vector2(dir, -1f);

        RaycastHit2D lateralHitCheck = Physics2D.Raycast(transform.position, dirHitVector, 1f, layerMaskToCollideCheck);
        RaycastHit2D surfaceHitCheck = Physics2D.Raycast(transform.position, surfaceVector, 2f, layerMaskToCollideCheck);

        Debug.DrawRay(coll.bounds.center, dirHitVector);
        Debug.DrawRay(coll.bounds.center, surfaceVector);

        if (surfaceHitCheck.collider == null && !changedDir)
        {
            dir *= -1;
            animator.SetFloat("DirX", dir * -1);
            
            changedDir = true;
        } else if (surfaceHitCheck.collider != null)
        {
            changedDir = false;
        }

        // If Ray Hits LayerMask should change direction
        if (lateralHitCheck.collider != null)
        {

            dir *= -1;
            animator.SetFloat("DirX", dir * -1);

        }

    }

    internal void Stop()
    {
        IsMoving = false;
        animator.SetBool("IsMoving", IsMoving);
    }

    internal void Resume()
    {
        IsMoving = true;
        animator.SetBool("IsMoving", IsMoving);
    }
}
