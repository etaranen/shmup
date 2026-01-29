using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int scoreValue = 50;
    public float moveSpeed = 2f;

    void Update()
    {
        // Move left like enemies
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        // Destroy if off screen
        if (transform.position.x < -12f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManagerScript.Instance.score += scoreValue;
            Destroy(gameObject);
        }
    }
}
