using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectionManager : MonoBehaviour
{
    public static PlayerSelectionManager Instance;

    public GameObject ship1;
    public GameObject ship2;

    public GameObject selectedShip;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
