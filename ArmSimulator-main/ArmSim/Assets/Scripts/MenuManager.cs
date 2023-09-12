using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        Application.LoadLevel("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
