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

    public void DrainFuel()
    {
        currentFuel-= fuelDecreasePerSecond * Time.deltaTime;
    }

    public void GainFuel(float amount)
    {
        currentFuel += amount;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death();
        }
    }

    public void HealDamage(float amount)
    {
        currentHealth += amount;
    }

    public void Death()
    {
        Instantiate(rocketGibs, transform.position, transform.rotation);
        Vector3 explosionPos = transform.up * -5;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 10f);
        foreach (var hit in colliders)
        {
            Rigidbody phys = hit.GetComponent<Rigidbody>();
            if (phys != null)
            {
                phys.AddExplosionForce(500, explosionPos, 30, 3);
            }
        }
        Destroy(gameObject);
    }
    
    private void Update()
    {
        if (currentFuel <= 0)
        {
            currentFuel = 0;
            noFuelLeft = true;
        }
    }
}