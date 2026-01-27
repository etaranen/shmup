using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectionManager : MonoBehaviour
{
    public static PlayerSelectionManager Instance;

    [Header("Available Ships")]
    public PlayerData ship1;
    public PlayerData ship2;

    [HideInInspector]
    public PlayerData selectedShip;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectShip1()
    {
        selectedShip = ship1;
    }

    public void SelectShip2()
    {
        selectedShip = ship2;
    }
}
