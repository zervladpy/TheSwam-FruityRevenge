using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    private static string DIE_TRIGGER = "Die";
    private static string DIRX_FLOAT = "DirX";
    private static string DIRY_FLOAT = "DirY";
    private static string ON_GROUND_BOOL = "OnGround";
    private static string ON_AIR_BOOL = "OnAir";
    private static string IS_MOVING_BOOL = "IsMoving";

    [SerializeField] public float DespawnTimer;
    [SerializeField] public CharacterStats Stats;
    [SerializeField] public CollisionManager CollisionManager;

    public Animator Animator { get; private set; }

    public bool IsDespawning { get; private set; }
    public bool IsActive { get; private set; }

    public float DirY { get; private set; }
    public float DirX { get; private set; }

    abstract public void Move();
    abstract public void OnAir();

    public void Start()
    {
        Animator = GetComponent<Animator>();

        IsDespawning = false;
        IsActive = true;
    }

    /// <summary>
    /// Recieves Damage
    /// </summary>
    public void ApplyDamage()
    {
        Animator.SetTrigger(DIE_TRIGGER);
        Despawn();
    }

    public void Update()
    {
        DirX = Animator.GetFloat(DIRX_FLOAT);
        DirY = Animator.GetFloat(DIRY_FLOAT);
        CheckGround();

        if (IsDespawning) return;

        if (IsActive)
        {
            Move();
            OnAir();

        }

    }

    public void Despawn()
    {
        if (IsDespawning) return;

        IsDespawning = true;
        StartCoroutine(AwaitDespawn());
    }

    public void CheckGround()
    {
        bool IsOnGround = CollisionManager.BottomCollision;

        Animator.SetBool(ON_GROUND_BOOL, IsOnGround);
        Animator.SetBool(ON_AIR_BOOL, !IsOnGround);
    }

    private IEnumerator AwaitDespawn()
    {
        yield return new WaitForSeconds(DespawnTimer);

        Debug.Log("Despawning");

        if (this.CompareTag("Hero"))
        {
            GameManager.Instance.RestLive();

        }

        Destroy(gameObject);
    }

    public void Stop()
    {
        IsActive = false;
        Animator.SetBool(IS_MOVING_BOOL, IsActive);

    }

    public void Resume()
    {
        IsActive = true;
    }
}
