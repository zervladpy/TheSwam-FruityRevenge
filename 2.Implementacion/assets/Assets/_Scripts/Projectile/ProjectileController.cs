using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] public ProjectileStats Stats;

    private Animator animator;
    private float Dir = 0f;
    private bool isMoving = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(new Vector2(Dir * Stats.Velocity, 0f) * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isMoving = false;
        animator.SetTrigger("Impact");

        if (collision.collider.CompareTag("Hero"))
        {
            collision.collider.GetComponent<PlayerController>().ApplyDamage();
        }
    }

    /// <summary>
    /// Called On Impact Animation Finish
    /// </summary>
    public void Despawn()
    {
        Destroy(gameObject);
    }

    public void SetParams(float DirX)
    {
        Dir = DirX;
    }
}
