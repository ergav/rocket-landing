using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{
    private UIManager _uiManager;

    private bool isPaused;
    private GameManager _gameManager;
    
    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
        UnPause();
    }

    public void PauseInput(InputAction.CallbackContext ctx)
    {
        if (_gameManager.playerIsDead || _gameManager.stageIsCleared)
        {
            return;
        }
        Debug.Log("Pausing");
        
        if (isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _uiManager.pauseMenu.SetActive(true);
        isPaused = true;
        _gameManager.UnLockCursor();
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        _uiManager.pauseMenu.SetActive(false);
        isPaused = false;
        _gameManager.LockCursor();
    }
}