using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject shipSelection;
    public GameObject instructions;
    public GameObject credits;
    
    public void StartGame()
    {
        if (PlayerSelectionManager.Instance == null)
        {
            Debug.LogError("PlayerSelectionManager missing BEFORE scene load!");
        }

        SceneManager.LoadScene("Stage1");
    }

    public void ShowShipSelection()
    {
        mainMenu.SetActive(false);
        shipSelection.SetActive(true);
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    public void ShowInstructions()
    {
        mainMenu.SetActive(false);
        shipSelection.SetActive(false);
        instructions.SetActive(true);
        credits.SetActive(false);
    }

    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        shipSelection.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        shipSelection.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
