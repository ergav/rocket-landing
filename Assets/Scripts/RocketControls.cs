using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketControls : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float rocketForce = 15f;
    [SerializeField] private float rocketSideForce = 10f;

    private bool rocketActive;

    [SerializeField] private float crashVelocity = 5f;

    [SerializeField] private Vector3 currentVelocity;

    private float leftRightMovement;
    private float forwardBackMovement;

    private RocketFuel _fuel;

    [SerializeField] private GameObject fire;

    [SerializeField] private float crashDamageMultiplier = 10;

    private RocketSoundManager _soundManager;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Vector3 groundCheckBoxSize;
    public bool isGrounded;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _fuel = GetComponent<RocketFuel>();
        _soundManager = GetComponent<RocketSoundManager>();
    }

    public void RocketInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            rocketActive = true;
        }
        else if (ctx.canceled)
        {
            rocketActive = false;
        }
    }

    public void RocketLeftRightInput(InputAction.CallbackContext ctx)
    {
        leftRightMovement = ctx.ReadValue<float>();
    }
    
    public void RocketForwardBackInput(InputAction.CallbackContext ctx)
    {
        forwardBackMovement = ctx.ReadValue<float>();
    }
    
    private void Update()
    {
        currentVelocity = rb.velocity;

        if (rocketActive && !_fuel.noFuelLeft)
        {
            _fuel.DrainFuel();
            fire.SetActive(true);
            _soundManager.PlayRocketNozzleSound();
        }
        else
        {
            fire.SetActive(false);
            _soundManager.StopRocketNozzleSound();
        }
        
        CheckGrounded();
    }

    public void Crash(float damage)
    {
        Debug.Log("Crash!");
        _fuel.TakeDamage(damage);
        if (_soundManager != null)
        {
            _soundManager.PlayCrashSound();
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (Mathf.Abs(currentVelocity.x) > crashVelocity)
        {
            Crash(crashDamageMultiplier * Mathf.Abs(currentVelocity.x));
        }
        if (Mathf.Abs(currentVelocity.y) > crashVelocity)
        {
            Crash(crashDamageMultiplier * Mathf.Abs(currentVelocity.y));
        }
        if (Mathf.Abs(currentVelocity.z) > crashVelocity)
        {
            Crash(crashDamageMultiplier * Mathf.Abs(currentVelocity.z));
        }
    }

    private void CheckGrounded()
    {
        //isGrounded = Physics.OverlapBox(groundCheck.position, groundCheckBoxSize, Quaternion.identity, groundLayerMask) != null;
        Collider[] hit = Physics.OverlapBox(groundCheck.position, groundCheckBoxSize, Quaternion.identity,
            groundLayerMask);
        if (hit.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    private void FixedUpdate()
    {
        if (rocketActive && !_fuel.noFuelLeft)
        {
            rb.AddForce(transform.up * rocketForce,ForceMode.Force);
        }

        if (!_fuel.noFuelLeft && !isGrounded)
        {
            rb.AddForce(Vector3.right * (leftRightMovement * rocketSideForce));
            rb.AddForce(Vector3.forward * (forwardBackMovement * rocketSideForce));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);
    }
}