using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawn;

    void Start()
    {
        if (PlayerSelectionManager.Instance == null)
        {
            return;
        }

        GameObject selected = PlayerSelectionManager.Instance.selectedShip;

        if (selected != null)
        {
            Instantiate(selected, playerSpawn.position, Quaternion.identity);
        }
    }
}
