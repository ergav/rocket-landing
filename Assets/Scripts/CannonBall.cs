using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private float damageToGive;
    private float projectileSpeed = 5;

    public void Initialize(float damage, float speed)
    {
        damageToGive = damage;
        projectileSpeed = speed;
    }

    private void Update()
    {
        transform.Translate(transform.forward * (projectileSpeed * Time.deltaTime));
    }
}
