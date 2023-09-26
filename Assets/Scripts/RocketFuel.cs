using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFuel : MonoBehaviour
{
    [SerializeField] private float currentFuel = 100;
    [SerializeField] private float maxFuel = 100;

    [SerializeField] private float currentHealth = 100;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float fuelDecreasePerSecond = 1;

    public bool noFuelLeft;

    [SerializeField] private GameObject rocketGibs;

    [HideInInspector] public bool fullyHealed;
    [HideInInspector] public bool fullyFueled;
    
    [SerializeField] private UIManager uiManager;
    private RocketSoundManager _soundManager;

    [SerializeField] private GameObject explosion;

    private GameManager _gameManager;

    private RocketControls _rocketControls;

    [HideInInspector] public bool inGoal;

    private bool isDead;
    
    private void Start()
    {
        _soundManager = GetComponent<RocketSoundManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _rocketControls = GetComponent<RocketControls>();
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    public void DrainFuel()
    {
        currentFuel -= fuelDecreasePerSecond * Time.deltaTime;
    }

    public void GainFuel(float amount)
    {
        currentFuel += amount;
        if (currentFuel >= maxFuel)
        {
            currentFuel = maxFuel;
        }
        if (_soundManager != null)
        {
            _soundManager.PlayRefuelSound();
        }

        //Just in case you manage to collect a gascan while fuel is empty.
        noFuelLeft = false;
    }

    public void TakeDamage(float amount)
    {
        if (_gameManager.stageIsCleared)
        {
            return;
        }
        
        currentHealth -= amount;
        Debug.Log(amount + " damage taken!");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (uiManager != null)
            {
                uiManager.healthBar.localScale = new Vector2((maxHealth / 100) * (currentHealth / 100), uiManager.healthBar.localScale.y);
            }

            //This is needed to prevent multiple gibs from spawning
            if (isDead)
            {
                return;
            }
            
            Death();
        }
    }

    public void HealDamage(float amount)
    {
        currentHealth += amount;
        
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (_soundManager != null)
        {
            _soundManager.PlayRepairSound();
        }
    }

    public void Death()
    {
        Instantiate(rocketGibs, transform.position, transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);

        if (_gameManager != null)
        {
            _gameManager.PlayerDeath();
        }

        isDead = true;
        Destroy(gameObject);
    }
    
    private void Update()
    {
        if (currentFuel <= 0)
        {
            currentFuel = 0;
            noFuelLeft = true;
        }

        if (currentHealth == maxHealth)
        {
            fullyHealed = true;
        }
        else
        {
            fullyHealed = false;
        }
        
        if (currentFuel == maxFuel)
        {
            fullyFueled = true;
        }
        else
        {
            fullyFueled = false;
        }
        
        if (uiManager != null)
        {
            uiManager.healthBar.localScale = new Vector2((maxHealth / 100) * (currentHealth / 100), uiManager.healthBar.localScale.y);
            uiManager.fuelBar.localScale = new Vector2((maxFuel / 100) * (currentFuel / 100), uiManager.fuelBar.localScale.y);
        }

        if (_gameManager != null)
        {
            if (noFuelLeft && !_gameManager.playerIsDead && _rocketControls.isGrounded && !inGoal)
            {
                _gameManager.PlayerDeath();
            }
        }
    }
}