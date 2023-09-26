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

    private GameManager gameManager;
    private ObjectPool objectPool;

    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<RocketControls>().transform;
        }

        gameManager = FindObjectOfType<GameManager>();
        objectPool = GetComponent<ObjectPool>();
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

        if (gameManager.stageIsCleared)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Linecast(transform.position, target.position, out hit))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return;
            }
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
        GameObject bullet = objectPool.GetPooledObject();

        if (bullet != null) 
        {
            CannonBall cannonBall = bullet.GetComponent<CannonBall>();
            cannonBall.Initialize(damageToGive, projectileSpeed);
            bullet.transform.position = projectileSpawnPoint.transform.position;
            bullet.transform.rotation = projectileSpawnPoint.transform.rotation;
            bullet.SetActive(true);
            PlayFireSound();
        }
        // CannonBall instantiatedCannonball = Instantiate(projectilePrefab.GetComponent<CannonBall>(), projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        // instantiatedCannonball.Initialize(damageToGive, projectileSpeed);
    }

    private void PlayFireSound()
    {
        int rng = Random.Range(0, fireSounds.Length);
        AudioSource.PlayClipAtPoint(fireSounds[rng], transform.position, 1);
    }
}