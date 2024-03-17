using JetBrains.Annotations;
using UnityEngine;


public class PlayerController : Character
{

    private const string HORIZONTAL = "Horizontal";
    private const string DIRX_FLOAT = "DirX";
    private const string DIRY_FLOAT = "DirY";
    private const string JUMP_TRIGGER = "Jump";
    private const string ON_AIR_BOOL = "OnAir";
    private const string DOUBLE_JUMP_BOOL = "DoubleJump";
    private const string ENEMY_TAG = "Enemy";

    private AudioSource AudioSource;

    new void Start()
    {
        base.Start();

        AudioSource = GetComponent<AudioSource>();
    }

    public override void OnAir()
    {
        bool DoubleJump = Animator.GetBool(DOUBLE_JUMP_BOOL);

        if (DirY > 0 && !DoubleJump)
        {
            Animator.SetTrigger(JUMP_TRIGGER);

            bool OnAir = Animator.GetBool(ON_AIR_BOOL);

            if (OnAir)
            {
                Animator.SetBool(DOUBLE_JUMP_BOOL, true);
            }
        }

        bool lateralCollision = CollisionManager.LeftCollision || CollisionManager.RightCollision;
        bool wasWalled = Animator.GetBool(PAP.WallCollision);

        if (!wasWalled)
        {
            Animator.SetTrigger(PAP.WallSliding);
        }

        Animator.SetBool(PAP.WallCollision, lateralCollision);
    }

    public override void Move()
    {

        float dirX = Input.GetAxisRaw(HORIZONTAL);

        Animator.SetFloat(DIRX_FLOAT, dirX);

        float dirY = Input.GetKeyDown(KeyCode.UpArrow) ? 1f : 0f;

        Animator.SetFloat(DIRY_FLOAT, dirY);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(ENEMY_TAG))
        {

            if (CollisionManager.LeftCollision)
            {
                ApplyDamage();
            }

            if (CollisionManager.RightCollision)
            {
                ApplyDamage();
            }

            if (CollisionManager.TopCollision)
            {
                ApplyDamage();
            }

        }

        if (collision.collider.CompareTag("Spikes"))
        {

            ApplyDamage();  
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.CompareTag("Flag"));

        if (collision.CompareTag("Flag"))
        {
            Animator.SetTrigger("DeSpawn");
        }
    }

    new public void ApplyDamage()
    {
        AudioSource.Play();

        GameManager.Instance.ResetScene();
        base.ApplyDamage();

    }

}
