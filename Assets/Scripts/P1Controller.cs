using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1Controller : MonoBehaviour
{
    //outside connections
    private Rigidbody _rb;
    private Camera _Camera;
    [SerializeField] private LayerMask _floorMask;

    //stats vars
    private readonly float _sprintSpeed = 3000f;
    private readonly float _walkSpeed = 1000f;
    private readonly float _sprintDUration = 0.15f;
    private readonly float _jumpVelocety = 20f;
    private readonly float _gravetyMultiplier = 3f;


    //script vars
    private float _moveSpeed;
    private float _sprintTimer;
    private bool _isSprinting;
    private Vector2 _moveInput;
    private Vector2 _rotateInput;
    private bool _primaryInput = false;
    private bool _isGrounded = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _moveSpeed = _walkSpeed;
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
        HandleSprint();
        ApplyGravety();
        UpdateMovement();
    }

    private void CheckIfGrounded()
    {
        float offset = 0.5f;
        Vector3 position = transform.position;
        position.y += offset; //add a offset so that the ray isnt cast in the ground itself
        if (Physics.Raycast(position, Vector3.down, offset + 0.2f, _floorMask)) //check if the charachter is on the ground by shooting a raycast, +0.2f so that its more consistant
        {
            _isGrounded = true;
            return;
        }
        _isGrounded = false;
    }

    private void HandleSprint()
    {
        if (_isSprinting)
        {
            _sprintTimer -= Time.deltaTime;
            if (_sprintTimer <= 0)
            {
                _isSprinting = false;
                _moveSpeed = _walkSpeed;
            }
        }
    }

    private void ApplyGravety()
    {
        _rb.linearVelocity += Vector3.up * Physics.gravity.y * Time.fixedDeltaTime * _gravetyMultiplier; //gravery mulktiplier is there bc it felt to floaty
    }

    private void UpdateMovement()
    {
        float xVelocety = _moveInput.x * _moveSpeed * Time.fixedDeltaTime;
        float zVelocety = _moveInput.y * _moveSpeed * Time.fixedDeltaTime;
        _rb.linearVelocity = new Vector3(xVelocety, _rb.linearVelocity.y, zVelocety);
    }

    //when joystick is moved update value
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    public void OnRotate(InputValue value)
    {
        _rotateInput = value.Get<Vector2>();
    }
    public void OnPrimary(InputValue value)
    {
        _primaryInput = value.Get<float>() > 0.5f;
    }
    public void OnSprint(InputValue value)
    {
        _isSprinting = true;
        _moveSpeed = _sprintSpeed;
        _sprintTimer = _sprintDUration;
    }
    public void OnJump(InputValue value)
    {
        if (!_isGrounded) return;
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpVelocety, _rb.linearVelocity.z);
    }
}
