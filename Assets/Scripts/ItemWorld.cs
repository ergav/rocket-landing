using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Item item;
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RocketFuel fuel = other.GetComponent<RocketFuel>();
            if (item.itemType == ItemType.Health)
            {
                fuel.HealDamage(item.healValue);
            }
            else if (item.itemType == ItemType.Fuel)
            {
                fuel.GainFuel(item.healValue);
            }
            Destroy(gameObject);
        }
    }
}
