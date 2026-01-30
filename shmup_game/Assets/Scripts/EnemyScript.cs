using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
  private bool hasSpawn;
  private MoveScript moveScript;
  private WeaponScript[] weapons;
  private Collider2D coliderComponent;
  private SpriteRenderer rendererComponent;

  public float dodgeDistance = 2f;
  public float dodgeSpeed = 5f;
  public float detectionRange = 5f;
  private bool isDodging = false;
  private Vector2 dodgeDirection;
  private Vector2 dodgeTarget;

  void Awake()
  {
    weapons = GetComponentsInChildren<WeaponScript>();
    moveScript = GetComponent<MoveScript>();
    coliderComponent = GetComponent<Collider2D>();
    rendererComponent = GetComponent<SpriteRenderer>();
  }

  void Start()
  {
      hasSpawn = false;

      coliderComponent.enabled = false;
      moveScript.enabled = false;
      foreach (WeaponScript weapon in weapons)
      {
          weapon.enabled = false;
      }
  }

  void Update()
  {
    if (hasSpawn == false)
    {
      if (rendererComponent.IsVisibleFrom(Camera.main))
      {
        Spawn();
      }
    }
    else
    {
      foreach (WeaponScript weapon in weapons)
      {
        if (weapon != null && weapon.enabled && weapon.CanAttack)
          {
            weapon.Attack(true);
          }
      }

      DetectProjectiles();

      if (isDodging)
      {
          transform.position = Vector2.MoveTowards(
              transform.position,
              dodgeTarget,
              dodgeSpeed * Time.deltaTime
          );

          if (Vector2.Distance(transform.position, dodgeTarget) < 0.1f)
          {
              isDodging = false;
          }
      }

      if (rendererComponent.IsVisibleFrom(Camera.main) == false)
        {
          Destroy(gameObject);
        }
    }
  }

  private void Spawn()
  {
    hasSpawn = true;

    coliderComponent.enabled = true;
    moveScript.enabled = true;

    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = true;
    }
  }

  void DetectProjectiles()
  {
    if (isDodging) return;

    GameObject[] projectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");

    foreach (GameObject proj in projectiles)
    {
        Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
        if (projRb == null) continue;

        Vector2 toEnemy = (Vector2)transform.position - projRb.position;
        float distance = toEnemy.magnitude;

        if (distance > detectionRange) continue;

        float dot = Vector2.Dot(projRb.velocity.normalized, toEnemy.normalized);

        if (dot > 0.9f)
        {
            Vector2 perpendicular = Vector2.Perpendicular(projRb.velocity).normalized;

            if (Random.value < 0.5f)
                perpendicular *= -1;

            dodgeTarget = (Vector2)transform.position + perpendicular * dodgeDistance;
            isDodging = true;
            break;
        }
    }
  }

  void OnDestroy()
  {
      if (ScoreManagerScript.Instance != null)
      {
          ScoreManagerScript.Instance.AddKillScore();
      }
  }
}
