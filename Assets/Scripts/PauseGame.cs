using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    private UIManager _uiManager;

    private bool isPaused;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        UnPause();
    }

    public void PauseInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("Pausing");
        if (isPaused)
        {
            UnPause();
            isPaused = false;
        }
        else
        {
            Pause();
            isPaused = true;
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        _uiManager.pauseMenu.SetActive(true);
    }

    private void UnPause()
    {
        Time.timeScale = 1;
        _uiManager.pauseMenu.SetActive(false);
    }
}
