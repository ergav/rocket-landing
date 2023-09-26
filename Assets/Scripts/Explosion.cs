using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float blastRadius = 25;
    [SerializeField] private float explosionForce = 250;
    [SerializeField] private float upwardsModifier = 10;

    [Space] 
    
    [SerializeField] private float blastFinalScale = 25;
    [SerializeField] private float blastScaleTime;
    private Transform blastEffect;
    private float t;
    
    [SerializeField] private AudioClip[] explodeSounds;

    private void Awake()
    {
        blastEffect = GetComponentInChildren<Transform>();
        int rng = Random.Range(0, explodeSounds.Length);
        AudioSource.PlayClipAtPoint(explodeSounds[rng], transform.position, 1);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach (var hit in colliders)
        {
            Rigidbody phys = hit.GetComponent<Rigidbody>();
            if (phys != null)
            {
                phys.AddExplosionForce(explosionForce, transform.position, blastRadius, upwardsModifier);
                Debug.Log("Kaboom!");
            }
        }
    }

    private void Update()
    {
        //Scaling the explosion effect game object
        Vector3 finalScale = new Vector3(blastFinalScale, blastFinalScale, blastFinalScale);
        blastEffect.localScale = Vector3.Lerp(blastEffect.localScale, finalScale, t);

        t += Time.deltaTime/blastScaleTime;

        if (blastEffect.localScale == finalScale)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}