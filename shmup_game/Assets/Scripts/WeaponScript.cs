using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
  public Transform shotPrefab;
  public Transform homingShotPrefab;

  public float shootingRate = 0.25f;
  private float shootCooldown;

  void Start()
  {
    shootCooldown = 0f;
  }

  void Update()
  {
    if (shootCooldown > 0)
    {
      shootCooldown -= Time.deltaTime;
    }
  }

  public void Attack(bool isEnemy, bool homing = false)
  {
    if (CanAttack)
    {
        shootCooldown = shootingRate;

        Transform prefabToUse = homing ? homingShotPrefab : shotPrefab;

        var shotTransform = Instantiate(prefabToUse) as Transform;

        shotTransform.position = transform.position;

        ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
        if (shot != null)
        {
            shot.isEnemyShot = isEnemy;
        }

        if (!homing)
        {
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
                move.direction = this.transform.right;
        }
    }
  }

  public bool CanAttack
  {
    get
    {
      return shootCooldown <= 0f;
    }
  }
}
