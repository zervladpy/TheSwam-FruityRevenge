using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    private GameManager gameManager;
    private const string TAG = "Hero";

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TAG))
        {
            gameManager.NextLevel();
        }
    }

}
