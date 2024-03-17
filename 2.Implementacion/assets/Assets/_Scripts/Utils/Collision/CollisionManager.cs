using UnityEngine;

/// <summary>
/// Simple Reused Collision Checker 
/// </summary>
public class CollisionManager : MonoBehaviour
{

    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float offset = 0.1f;

    private Collider2D coll;

    private float rayLenght;

    public bool LeftCollision { get; private set; }
    public bool RightCollision { get; private set; }
    public bool TopCollision { get; private set; }
    public bool BottomCollision { get; private set; }

    public Collider2D Collider { get; private set; }

    private void Start()
    {
        coll = GetComponent<Collider2D>();

        rayLenght = coll.bounds.size.x / 2 + offset;
    }

    void Update()
    {
        LeftCollision = LColl();
        RightCollision = RColl();
        TopCollision = TColl();
        BottomCollision = BColl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider = collision.collider;
    }

    private bool LColl() 
    {
        return Physics2D.Raycast(coll.bounds.center, Vector2.left, rayLenght, collisionMask).collider != null;
    }

    private bool RColl()
    {
        return Physics2D.Raycast(coll.bounds.center, Vector2.right, rayLenght, collisionMask).collider != null;
    }

    private bool TColl()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.up, offset, collisionMask);
    }

    private bool BColl()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, offset, collisionMask);
    }
}
