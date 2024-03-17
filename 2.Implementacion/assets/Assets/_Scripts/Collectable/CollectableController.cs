using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{

    [SerializeField]
    public BaseCollectable collectable;


    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private AudioSource AudioSource;

    private const string COLLECTED_TRIGGER = "Collected";
    private bool scoreAddded = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
        gameManager = GameManager.Instance;

        spriteRenderer.sprite = collectable.idle;
        animator.runtimeAnimatorController = collectable.animationController;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!scoreAddded) {
            AudioSource.Play();
            animator.SetTrigger(COLLECTED_TRIGGER);
            gameManager.AddScore(collectable.points);
            scoreAddded = true;
        }
    }

    /// <summary>
    /// Called From Animation when is completed
    /// </summary>
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
