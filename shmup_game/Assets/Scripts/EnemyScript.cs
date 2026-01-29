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
          transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dodgeDirection, dodgeSpeed * Time.deltaTime);

          if (Vector2.Distance(transform.position, (Vector2)transform.position - dodgeDirection) >= dodgeDistance)
              isDodging = false;
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
          Vector2 toProjectile = proj.transform.position - transform.position;

          Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
          if (projRb == null) continue;

          Vector2 projVelocity = projRb.velocity;
          float approach = Vector2.Dot(projVelocity.normalized, toProjectile.normalized);

          if (toProjectile.magnitude < detectionRange && approach > 0.8f) 
          {
              dodgeDirection = Vector2.Perpendicular(projVelocity).normalized * dodgeDistance;

              if (Random.value < 0.5f) dodgeDirection *= -1;

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
