using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        blastEffect = GetComponentInChildren<Transform>();
        Vector3 explosionPos = transform.up * -5;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 10f);
        foreach (var hit in colliders)
        {
            Rigidbody phys = hit.GetComponent<Rigidbody>();
            if (phys != null)
            {
                phys.AddExplosionForce(explosionForce, explosionPos, blastRadius, upwardsModifier);
            }
        }
    }

    private void Update()
    {
        Vector3 finalScale = new Vector3(blastFinalScale, blastFinalScale, blastFinalScale);
        blastEffect.localScale = Vector3.Lerp(blastEffect.localScale, finalScale, t);

        t += Time.deltaTime/blastScaleTime;

        if (blastEffect.localScale == finalScale)
        {
            Destroy(gameObject);
        }
    }
}
