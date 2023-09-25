using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTurret : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private Transform target;
    [SerializeField] private float targetDetectionRange = 100;
    [SerializeField] private Transform turretBody;
    [SerializeField] private float fireCooldown = 5;
    [SerializeField] private float projectileSpeed = 5;
    [SerializeField] private float damageToGive = 20;
    [SerializeField] private AudioClip[] fireSounds;

    private float timer;

    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<RocketControls>().transform;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, target.position) > targetDetectionRange)
        {
            return;
        }
        
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretBody.forward, targetDir, rotationSpeed * Time.deltaTime, 0);
        turretBody.rotation = Quaternion.LookRotation(newDir);

        timer += Time.deltaTime;
        if (timer >= fireCooldown)
        {
            timer = 0;
            FireProjectile();
        }
    }

    private void FireProjectile()
    {
        CannonBall instantiatedCannonball = Instantiate(projectilePrefab.GetComponent<CannonBall>(), projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        instantiatedCannonball.Initialize(damageToGive, projectileSpeed);
        PlayFireSound();
    }

    private void PlayFireSound()
    {
        int rng = Random.Range(0, fireSounds.Length);
        AudioSource.PlayClipAtPoint(fireSounds[rng], transform.position, 1);

    }
}
