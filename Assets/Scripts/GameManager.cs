using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private float deathMenuDisplayDelay = 1;

    private bool playerIsDead;
    private float timer;
    private UIManager _uiManager;

    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<RocketControls>().gameObject;
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
    }

    public void PlayerDeath()
    {
        playerIsDead = true;
    }
}
