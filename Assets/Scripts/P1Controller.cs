using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

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
    [SerializeField]private  float _walkSpeedSideways = 1000f;
    [SerializeField] private  float _walkSpeedForward = 300f; //speed difference with floor (if 0 player will go as fast as floor)
    [SerializeField]private  float _dashTimeout = 5f; //time it takes to recharge dash
   [SerializeField] private float _dashVelocity = 100f;
    private readonly float _jumpVelocity = 30f;
    private readonly float _gravityMultiplier = 120f;

    //script vars
    private Vector2 _moveInput;
    private bool _primaryInput = false;
    private float _timeSinceLastDash = 0f;
    private Vector3 _currentDashVelocity = Vector3.zero;
    private float _moveSpeedMultiplier = 1;

    private void Awake()
    {
        Instance = this;
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
        _floorManager = FindFirstObjectByType<FloorManager>();
        Camera camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if(GameStateScript.Instance.StartTimer > 0 )return;
        _timeSinceLastDash += Time.fixedDeltaTime;
        UpdateButtonInputs();
        UpdateMovement();
    }

    public void PickUpKey()
    {
        OnPickUpKey?.Invoke(this, EventArgs.Empty);
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
        if (Physics.Raycast(transform.position, Vector3.forward, 0.9f + Mathf.Max(0, _rb.linearVelocity.z / 100), _obstacleMask) && inputs.z > 0) //added [0, _rb.linearVelocity.z/100], it does + vel.z/100 when its positive (faster = look more ahead)
        {
            inputs = new Vector3(inputs.x, inputs.y, 0);
            _currentDashVelocity = new Vector3(_currentDashVelocity.x, _currentDashVelocity.y, 0);
        }//if moving backwards but there is an obstacle, player move backwards is turned off
        if (Physics.Raycast(transform.position, Vector3.back, 0.9f, _obstacleMask) && inputs.z < 0)
        {
            inputs = new Vector3(inputs.x, inputs.y, 0);
        }
        //if left but there is an obstacle, player move left is turned off
        if (Physics.Raycast(transform.position, Vector3.left, 0.9f, _obstacleMask) && inputs.x < 0)
        {
            inputs = new Vector3(0, inputs.y, inputs.z);
        }
        //if right but there is an obstacle, player move right is turned off
        else if (Physics.Raycast(transform.position, Vector3.right, 0.9f, _obstacleMask) && inputs.x > 0)
        {
            inputs = new Vector3(0, inputs.y, inputs.z);
        }

        //if on ground
        if (Physics.SphereCast(transform.position + new Vector3(0, 0.5f, 0), 0.5f, Vector3.down, out RaycastHit hit, 0.5f, _floorMask))
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            gravity = Vector3.zero;

            //set player at ground level
            _rb.MovePosition(new Vector3(_rb.position.x, hit.point.y + 0.05f, _rb.position.z));
        }
        //apply forces by input
        _rb.linearVelocity = (inputs + Vector3.up * _rb.linearVelocity.y + gravity + _currentDashVelocity) * _moveSpeedMultiplier + track;

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
        _currentDashVelocity.x = _moveInput.normalized.x;
        _currentDashVelocity.z = _moveInput.normalized.y;
        _currentDashVelocity.y = 0;
        _currentDashVelocity *= _dashVelocity;
        GamepadManager.Instance.RumbleController(1, 0.15f, 0.05f);
    }
    private void TryJump()
    {
        //if no ground found dont jump
        if (!Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector3.down, 0.55f, _floorMask))
            return;

        //found ground thus jump
        _rb.AddForce(Vector3.up * _jumpVelocity, ForceMode.Impulse);
    }

    //when joystick is moved update value
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }
    public void OnPrimary(InputValue value)
    {
        _primaryInput = value.Get<float>() > 0.5f;
    }
    public void SetMoveSpeedMultiplier(float multiplier)
    {
        _moveSpeedMultiplier = multiplier;
    }
    private void UpdateButtonInputs()
    {
        //OnPrimary would only be called when pressed down, not when released, thus we need to check it in update
        _primaryInput = GetComponent<PlayerInput>().actions["Primary"].IsPressed();
        if (GetComponent<PlayerInput>().actions["Dash"].IsPressed())
            TryDash();
        if (GetComponent<PlayerInput>().actions["Jump"].IsPressed())
            TryJump();
    }
    public float GetDashCooldown()
    {
        return _timeSinceLastDash / _dashTimeout;
    }
}
