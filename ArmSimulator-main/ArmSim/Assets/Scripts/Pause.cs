using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject _pauseMenu;
    public GameObject _playUI;
    private bool _isPaused;

    private void Start() {
        _isPaused = false;
        _pauseMenu.SetActive(_isPaused);
    }

    private void Update() {
        
        Show_HideMenu();
    }

    private void Show_HideMenu()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     _isPaused = !_isPaused;
        // }
        _pauseMenu.SetActive(_isPaused);
        _playUI.SetActive(!_isPaused);
        if(_isPaused)
        {
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }

    public void PausedContinue()
    {
        _isPaused = false;
    }

    public void Paused()
    {
        _isPaused = true;
    }

    public void ToMenu()
    {
        Application.LoadLevel("Menu");
    }
}
