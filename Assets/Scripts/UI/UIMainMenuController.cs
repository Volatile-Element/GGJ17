using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game View");
    }

    public void ShowHelp()
    {
        //TODO: Show help.
    }

    public void HideHelp()
    {
        //TODO: Hide help.
    }

    public void Quit()
    {
        Application.Quit();
    }
}
