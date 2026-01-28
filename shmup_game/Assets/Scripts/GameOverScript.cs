using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// Start or quit the game
public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject background;
    public Button restartButton;
    public Button menuButton;
    public GameObject menuText;

    void Awake()
    {
        background.SetActive(false);
        restartButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);
        menuText.gameObject.SetActive(false);
    }

    public void ShowMenu()
    {
        background.SetActive(true);
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        menuText.gameObject.SetActive(true);
    }

    public void ExitToMenu()
    {
        // Reload the level
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        // Reload the level
        SceneManager.LoadScene("Stage1");
    }
}
