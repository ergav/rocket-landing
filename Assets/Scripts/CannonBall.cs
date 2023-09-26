using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private float damageToGive;
    private float projectileSpeed = 5;
    [SerializeField] private float lifespan = 10;

    private float timer;

    public void Initialize(float damage, float speed)
    {
        damageToGive = damage;
        projectileSpeed = speed;
        timer = 0;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * (projectileSpeed * Time.deltaTime));

        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            gameObject.SetActive(false);
        }
    }

    private void Impact()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            RocketFuel rocketFuel = other.collider.GetComponent<RocketFuel>();
            if (rocketFuel != null)
            {
                rocketFuel.TakeDamage(damageToGive);
            }
        }
        Impact();
    }
}