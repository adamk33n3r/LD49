using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject instructions;
    public void Start()
    {
        ShowInstructions(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowInstructions(bool show)
    {
        this.instructions.SetActive(show);
    }
}
