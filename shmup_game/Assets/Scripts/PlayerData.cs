using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Shmup/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string shipName;
    public GameObject shipPrefab;
    public float speed = 50f;
    public int health = 3;
    public int projectileDamage = 1;
}
