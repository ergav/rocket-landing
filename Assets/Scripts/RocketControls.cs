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

    [SerializeField] private Transform mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 forwardRelative;
    private Vector3 sideRelative;
    private Vector3 moveDir;

    [SerializeField] private float currentSpeed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _fuel = GetComponent<RocketFuel>();
        _soundManager = GetComponent<RocketSoundManager>();
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>().transform;
        }
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

    //This is to prevent the rocket nozzle from still being active when the player reaches the goal.
    public void DeactivateRocket()
    {
        rocketActive = false;
        fire.SetActive(false);
        _soundManager.StopRocketNozzleSound();
        this.enabled = false;
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
        currentSpeed = rb.velocity.magnitude;
        
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

        camForward = mainCamera.forward;
        camRight = mainCamera.right;

        camRight.y = 0;
        camForward.y = 0;

        forwardRelative = forwardBackMovement * camForward;
        sideRelative = leftRightMovement * camRight;

        moveDir = forwardRelative + sideRelative;
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
        if (other.gameObject.CompareTag("Projectile"))
        {
            return;
        }
        if (currentSpeed > crashVelocity)
        {
            Crash(crashDamageMultiplier * currentSpeed);
        }
    }

    private void CheckGrounded()
    {
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
            rb.AddForce(moveDir.x * rocketSideForce, 0, moveDir.z * rocketSideForce);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBoxSize);
    }
}