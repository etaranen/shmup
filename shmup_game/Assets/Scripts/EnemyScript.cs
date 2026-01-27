using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Enemy generic behavior
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
    // Retrieve the weapon only once
    weapons = GetComponentsInChildren<WeaponScript>();

    // Retrieve scripts to disable when not spawn
    moveScript = GetComponent<MoveScript>();

    coliderComponent = GetComponent<Collider2D>();

    rendererComponent = GetComponent<SpriteRenderer>();
  }

  // Disable everything
  void Start()
  {
      hasSpawn = false;

      // Disable everything
      // -- collider
      coliderComponent.enabled = false;
      // -- Moving
      moveScript.enabled = false;
      // -- Shooting
      foreach (WeaponScript weapon in weapons)
      {
          weapon.enabled = false;
      }
  }

  void Update()
    {
      // Check if the enemy has spawned.
      if (hasSpawn == false)
      {
        if (rendererComponent.IsVisibleFrom(Camera.main))
        {
          Spawn();
        }
      }
      else
      {
        // Auto-fire
        foreach (WeaponScript weapon in weapons)
        {
          if (weapon != null && weapon.enabled && weapon.CanAttack)
            {
              weapon.Attack(true);
            }
        }

        // Dodging bullets
        DetectProjectiles();

        if (isDodging)
          {
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + dodgeDirection, dodgeSpeed * Time.deltaTime);

            // Stop dodging once we've moved dodgeDistance
            if (Vector2.Distance(transform.position, (Vector2)transform.position - dodgeDirection) >= dodgeDistance)
                isDodging = false;
          }

        // Out of the camera ? Destroy the game object.
        if (rendererComponent.IsVisibleFrom(Camera.main) == false)
          {
            Destroy(gameObject);
          }
      }
    }

  // Activate itself.
  private void Spawn()
  {
    hasSpawn = true;

    // Enable everything
    // -- Collider
    coliderComponent.enabled = true;
    // -- Moving
    moveScript.enabled = true;
    // -- Shooting
    foreach (WeaponScript weapon in weapons)
    {
      weapon.enabled = true;
    }
  }

  void DetectProjectiles()
    {
        // Only react if not already dodging
        if (isDodging) return;

        // Detect all player projectiles in scene
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");

        foreach (GameObject proj in projectiles)
        {
            Vector2 toProjectile = proj.transform.position - transform.position;

            // Check if projectile is in detection range and approaching
            Rigidbody2D projRb = proj.GetComponent<Rigidbody2D>();
            if (projRb == null) continue;

            Vector2 projVelocity = projRb.velocity;
            float approach = Vector2.Dot(projVelocity.normalized, toProjectile.normalized);

            // If projectile is coming roughly towards enemy
            if (toProjectile.magnitude < detectionRange && approach > 0.8f) 
            {
                // Pick dodge direction perpendicular to projectile
                dodgeDirection = Vector2.Perpendicular(projVelocity).normalized * dodgeDistance;

                // Randomly choose left/right dodge
                if (Random.value < 0.5f) dodgeDirection *= -1;

                isDodging = true;
                break; // Only dodge one projectile at a time
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
