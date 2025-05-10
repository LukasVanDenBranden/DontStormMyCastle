using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1Controller : MonoBehaviour
{
    //outside connections
    public EventHandler OnPickUpKey;
    public static P1Controller Instance;
    private Rigidbody _rb;
    private Camera _camera;
    private FloorManager _floorManager;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _floorMask;

    //stats vars
    private readonly float _walkSpeedSideways = 1000f;
    private readonly float _walkSpeedForward = 300f; //speed difference with floor (if 0 player will go as fast as floor)
    private readonly float _dashTimeout = 5f; //time it takes to recharge dash
    private readonly float _dashVelocity = 100f;
    private readonly float _jumpVelocity = 40f;
    private readonly float _gravityMultiplier = 120f;

    //script vars
    private Vector2 _moveInput;
    private Vector2 _rotateInput;
    private bool _primaryInput = false;
    private float _timeSinceLastDash = 0f;
    private Vector3 _currentDashVelocity = Vector3.zero;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
        _floorManager = FindFirstObjectByType<FloorManager>();
    }

    private void FixedUpdate()
    {
        _timeSinceLastDash += Time.fixedDeltaTime;
        UpdateButtonInputs();
        UpdateMovement();
    }

    public void PickUpKey()
    {
        OnPickUpKey?.Invoke(this,EventArgs.Empty);
    }

    private void UpdateMovement()
    {
        //get input force
        Vector3 inputs = new Vector3(_moveInput.x * _walkSpeedSideways * Time.fixedDeltaTime, 0, _moveInput.y * _walkSpeedForward * Time.fixedDeltaTime + _moveInput.y * _floorManager.GetFloorSpeed());

        //track
        Vector3 track = new Vector3(0, 0, -_floorManager.GetFloorSpeed());
        //get gravity
        Vector3 gravity = _gravityMultiplier * Time.fixedDeltaTime * Vector3.down;

        //if moving forward but there is an obstacle in front, player move forward is turned off
        if(Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.forward, 0.8f, _obstacleMask) && inputs.z > 0)
        {
            inputs = new Vector3(inputs.x, inputs.y, 0);
        }
        //if on ground
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.down, 0.52f, _floorMask))
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            gravity = Vector3.zero;
        }
        //apply forces by input
        _rb.linearVelocity = inputs + track + Vector3.up * _rb.linearVelocity.y + gravity + _currentDashVelocity;

        //add drag
        _currentDashVelocity /= 1.2f;
    }
    private void TryDash()
    {
        if (_timeSinceLastDash < _dashTimeout)
            return;
        //reset time
        _timeSinceLastDash = 0;
        //dash
        _currentDashVelocity = Vector3.forward * _dashVelocity;
    }
    private void TryJump()
    {

        //if no ground found dont jump
        if(!Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.down, 0.55f, _floorMask))
            return;

        //found ground thus jump
        _rb.AddForce(Vector3.up * _jumpVelocity, ForceMode.Impulse);
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
    private void UpdateButtonInputs()
    {
        //OnPrimary would only be called when pressed down, not when released, thus we need to check it in update
        _primaryInput = GetComponent<PlayerInput>().actions["Primary"].IsPressed();
        if (GetComponent<PlayerInput>().actions["Dash"].IsPressed())
            TryDash();
        if(GetComponent<PlayerInput>().actions["Jump"].IsPressed())
            TryJump();
    }
}
