using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameManager _manager;
    [SerializeField] private float goalCounter = 3;
    private bool playerInGoal;
    private float timer;

    private void Awake()
    {
        _manager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInGoal = true;
            RocketFuel rocketFuel = other.GetComponent<RocketFuel>();
            rocketFuel.inGoal = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInGoal = false;
            timer = 0;
            
            RocketFuel rocketFuel = other.GetComponent<RocketFuel>();
            rocketFuel.inGoal = false;
        }    
    }

    private void Update()
    {
        if (playerInGoal && !_manager.playerIsDead)
        {
            timer += Time.deltaTime;
            if (timer >= goalCounter && !_manager.stageIsCleared)
            {
                _manager.StageCleared();
            }
        }
    }
}