using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject shipSelection;
    public GameObject instructions;
    public GameObject credits;
    
    public void StartGame()
    {
        // "Stage1" is the name of the first scene.
        Application.LoadLevel("Stage1");
    }

    // Show Ship Selection screen
    public void ShowShipSelection()
    {
        mainMenu.SetActive(false);
        shipSelection.SetActive(true);
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    // Show Instructions screen
    public void ShowInstructions()
    {
        mainMenu.SetActive(false);
        shipSelection.SetActive(false);
        instructions.SetActive(true);
        credits.SetActive(false);
    }

    // Show Credits screen
    public void ShowCredits()
    {
        mainMenu.SetActive(false);
        shipSelection.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(true);
    }

    // Back to Main Menu
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        shipSelection.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    // Quit Game
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // shows in editor
        Application.Quit();
    }
}
