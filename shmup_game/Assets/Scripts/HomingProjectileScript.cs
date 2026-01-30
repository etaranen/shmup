using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 6f;

    private Transform target;

    void Start()
    {
        Destroy(gameObject, lifetime);
        FindTarget();
    }

    void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            FindTarget();
        }

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (GameObject e in enemies)
        {
            if (!e.activeInHierarchy) continue;
            float distance = Vector2.Distance(transform.position, e.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = e.transform;
            }
        }

        target = closestEnemy;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
