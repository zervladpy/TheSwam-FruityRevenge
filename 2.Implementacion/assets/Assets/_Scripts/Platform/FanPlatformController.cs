using System.Collections;
using UnityEngine;

public class FanPlatformController : MonoBehaviour
{

    [SerializeField]
    public float fallDelay;

    [SerializeField]
    public float fallSpeed;

    private Rigidbody2D body;
    private Animator animator;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("Fall");
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        animator.SetTrigger("Pressed");

        body.bodyType = RigidbodyType2D.Dynamic;

    }

}
