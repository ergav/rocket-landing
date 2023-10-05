using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RocketControls player;

    [SerializeField] private float deathMenuDisplayDelay = 1;

    [HideInInspector]public bool playerIsDead;
    [HideInInspector]public bool stageIsCleared;
    private float timer;
    private UIManager _uiManager;

    private CinemachineFreeLook _cinemachineFreeLook;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<RocketControls>();
        }

        _uiManager = FindObjectOfType<UIManager>();
        _cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        
        LockCursor();
    }

    private void Update()
    {
        if (playerIsDead)
        {
            timer += Time.deltaTime;

            if (timer >= deathMenuDisplayDelay)
            {
                _uiManager.gameOverMenu.SetActive(true);
            }
        }

        if (stageIsCleared)
        {
            player.DeactivateRocket();
        }
    }

    public void PlayerDeath()
    {
        playerIsDead = true;
        _cinemachineFreeLook.enabled = false;
        UnLockCursor();
    }

    public void StageCleared()
    {
        _uiManager.goalMenu.SetActive(true);
        stageIsCleared = true;
        _cinemachineFreeLook.enabled = false;
        UnLockCursor();
    }

    public void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnLockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}