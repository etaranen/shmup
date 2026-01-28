using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerSpawn;

    void Start()
    {
        Debug.Log("GameManager started");

        if (PlayerSelectionManager.Instance == null)
        {
            Debug.LogError("No PlayerSelectionManager found!");
            return;
        }

        GameObject selected = PlayerSelectionManager.Instance.selectedShip;

        if (selected != null)
        {
            Debug.Log("Spawning ship: " + selected.name);
            Instantiate(selected, playerSpawn.position, Quaternion.identity);
        }
    }
}
