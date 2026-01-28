using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
  // Designer variables

  /// Damage inflicted
  public int damage;

  /// Projectile damage player or enemies?
  public bool isEnemyShot = false;

  void Start()
  {
    // Limited time to live to avoid any leak
    Destroy(gameObject, 20); // 20sec
  }
}
