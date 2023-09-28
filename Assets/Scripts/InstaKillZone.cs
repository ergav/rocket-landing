using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RocketFuel fuel = other.GetComponent<RocketFuel>();
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager.stageIsCleared)
            {
                return;
            }
            fuel.Death();
        }
    }
}
