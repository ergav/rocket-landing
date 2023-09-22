using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RocketControls player;

    [SerializeField] private float deathMenuDisplayDelay = 1;

    [HideInInspector]public bool playerIsDead;
    [HideInInspector]public bool stageIsCleared;
    private float timer;
    private UIManager _uiManager;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<RocketControls>();
        }

        _uiManager = FindObjectOfType<UIManager>();
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
            player.enabled = false;
        }
    }

    public void PlayerDeath()
    {
        playerIsDead = true;
    }

    public void StageCleared()
    {
        _uiManager.goalMenu.SetActive(true);
        stageIsCleared = true;
    }
}