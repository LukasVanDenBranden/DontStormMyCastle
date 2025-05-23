using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class P2Controller : MonoBehaviour
{

    //outside connections
    private Rigidbody _rb;
    private Camera _camera;
    private RectTransform _primaryChargeUI;
    [SerializeField] private List<GameObject> _boulderPrefabList;
    private List<GameObject> _boulderList;
    public static P2Controller instance;
    [SerializeField] private float _primaryMaxThrowForce = 30f;

    //stats vars
    private readonly float _moveSpeed = 1000f;
    private readonly float _primaryThrowTime = 2f; //time it takes to reach max throwing force in seconds
    private readonly float _boulderDespawnYLevel = -10f;
    [SerializeField] private float _boulderCooldown = 1f; //cooldown until next boulder can be charged
    private readonly float _cameraDollyZoomStrength = 5f;
    private readonly int _chargeSpeedMultiplier = 2;
    public float Sensitivity = 100;

    //script vars
    private Vector2 moveInput;
    private Vector2 _rotateInput;
    private Quaternion _trowRotation;
    private bool _primaryInput = false;
    private float _primaryThrowForce = 0f;
    private float _currentRotationY = 0f;
    private GameObject _currentThrowingBoulder;
    private int _nextBoulderIndex = 0; //index of prefab list
    private float _boulderCooldownTimer;
    private float _cameraDefaultDistance;
    private float _cameraDefaultFOV;
    private float _chargePercentage = 0f; //[0, 1] to how much is charged AND goes slowely down using cooldown
    private Transform _aimingArrow;
    private int _heldBoulderIndex;
    private float _lastTrowAngle;
    private Vector3 _lastTrowDirection;
    private Quaternion _lastTrowRotation;

    private void Awake()
    {
        instance = this;
        _aimingArrow = GameObject.Find("AimingArrow").transform;
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
        _primaryChargeUI = GameObject.Find("PrimaryForceCharge").GetComponent<RectTransform>();
        _boulderList = new List<GameObject>();
    }

    private void Start()
    {
        _currentRotationY = -transform.forward.y;
        _boulderCooldownTimer = _boulderCooldown;
        _cameraDefaultDistance = _camera.transform.localPosition.z;
        _cameraDefaultFOV = _camera.fieldOfView;
        _rotateInput = transform.forward;
        _lastTrowRotation =transform.rotation;
        _aimingArrow.rotation = transform.rotation;
    }
    private void FixedUpdate()
    {
        if (GameStateScript.Instance.StartTimer > 0) return;

        UpdateButtonInputs();

        UpdateMovement();
        UpdatePrimary();

        //update percentage [0, 1] and is used multiple times in this script
        _chargePercentage = Mathf.MoveTowards(_chargePercentage, _primaryThrowForce / _primaryMaxThrowForce, 10 * Time.fixedDeltaTime);
        GamepadManager.Instance.RumbleController(2, (_chargePercentage * _chargePercentage) / 10, 0.1f);

        //UpdateCamera();
        UpdateUI();
        CleanUpBoulders();
    }
    private void UpdateMovement()
    {
        //add forces by input
        _rb.linearVelocity = new Vector3(-moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, 0);
        float x = Mathf.Clamp(transform.position.x, -18, 18);
        _aimingArrow.position = new Vector3(x, _aimingArrow.position.y, _aimingArrow.position.z);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void UpdatePrimary()
    {
        _boulderCooldownTimer -= Time.deltaTime;
        Vector3 throwDirection = GetInPutDirection();
        if (_boulderCooldownTimer >= 0) return;



        if (!_primaryInput)
        {
            //was throwing and now released, thus throw the boulder
            if (_currentThrowingBoulder != null)
            {
                _currentThrowingBoulder.GetComponent<MeshRenderer>().enabled = true;
                _currentThrowingBoulder.GetComponent<Rigidbody>().AddForce(throwDirection * _primaryThrowForce, ForceMode.Impulse); //add force with same direction player is looking
                //reset for next boulder
                _currentThrowingBoulder = null;
                _primaryThrowForce = 0f;
                _boulderCooldownTimer = _boulderCooldown;
            }
            return;
        }

        //in front of player with the magic number being the distance from player
        Vector3 boulderPos = transform.position + -transform.forward.normalized * 7.5f;
        boulderPos.y = 4.01f; //4 is half of the boulders height
        //if no boulder (first loop when button is pressed) spawn boulder, otherwise update its position
        if (_currentThrowingBoulder == null)
        {
            _currentThrowingBoulder = Instantiate(_boulderPrefabList[_nextBoulderIndex], boulderPos, Quaternion.identity); //spawn boulder
            _currentThrowingBoulder.GetComponent<MeshRenderer>().enabled = false;
            _trowRotation.y = 0;
            _nextBoulderIndex = 0; //reset so next boulder is just a normal boulder (unless changed)
        }
        else
            _currentThrowingBoulder.transform.position = boulderPos;
        _boulderList.Add(_currentThrowingBoulder);
        _currentThrowingBoulder.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;//stop boulder from accumilating falling speed
        //charge throw force
        _primaryThrowForce += _primaryMaxThrowForce * (Time.fixedDeltaTime / _primaryThrowTime) * _chargeSpeedMultiplier;
        //clamp throwforce
        if (_primaryThrowForce > _primaryMaxThrowForce)
            _primaryThrowForce = _primaryMaxThrowForce;
    }

    private Vector3 GetInPutDirection()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 trowDirection = forward * _rotateInput.y + right * _rotateInput.x;

        if (_rotateInput.magnitude > 0.4f) // deadzone
        {
            Quaternion targetRotation = Quaternion.LookRotation(trowDirection.normalized);
            float rotationy = targetRotation.eulerAngles.y - 180;
            
            float clamp = Mathf.Clamp(rotationy, -70, 70) + 180;
            targetRotation = Quaternion.Euler(0, clamp, 0);
            _lastTrowRotation = Quaternion.RotateTowards(_lastTrowRotation, targetRotation, _rotateInput.magnitude * Sensitivity * Time.deltaTime);
        }

        _aimingArrow.rotation = _lastTrowRotation;
        _lastTrowDirection = _lastTrowRotation * Vector3.forward;
        return _lastTrowDirection;
    }

    private void UpdateCamera()
    {
        //get new distance and FOV
        float newDistance = _cameraDefaultDistance + _cameraDollyZoomStrength * _chargePercentage;
        float newFOV = _cameraDefaultFOV + _cameraDollyZoomStrength * _chargePercentage;

        //apply
        _camera.transform.localPosition = new Vector3(_camera.transform.localPosition.x, _camera.transform.localPosition.y, newDistance);
        _camera.fieldOfView = Mathf.Clamp(newFOV, 1f, 179f);
    }
    private void UpdateUI()
    {
        //temp magic value (height/2 so it starts at bottom)
        _primaryChargeUI.anchoredPosition = new Vector3(0, 23.75f * _chargePercentage - 23.75f, 0);
        _primaryChargeUI.sizeDelta = new Vector2(_primaryChargeUI.sizeDelta.x, 47.5f * _chargePercentage);
    }
    private void CleanUpBoulders()
    {
        for (int i = _boulderList.Count - 1; i >= 0; i--)
        {
            if (_boulderList[i] == null)
            {
                _boulderList.RemoveAt(i);
                continue;
            }
            if (_boulderList[i].transform.position.y < _boulderDespawnYLevel)
            {
                Destroy(_boulderList[i]);
                _boulderList.RemoveAt(i);
            }
        }
    }
    public Vector3 PredictLandingPoint(Vector3 initialPosition, Vector3 initialVelocity, float targetY)
    {
        float a = 0.5f * Physics.gravity.y;
        float b = initialVelocity.y;
        float c = initialPosition.y - targetY;

        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
            return Vector3.zero; // will never hit the plane

        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        float tImpact = Mathf.Max(t1, t2);

        if (tImpact < 0)
            return Vector3.zero; // negative time — invalid

        Vector3 horizontalVelocity = new Vector3(initialVelocity.x, 0, initialVelocity.z);
        Vector3 landingPosition = initialPosition + horizontalVelocity * tImpact;
        landingPosition.y = targetY;
        return landingPosition;
    }
    //when gamepad changes a value save that new value
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void OnRotate(InputValue value)
    {
        _rotateInput = value.Get<Vector2>();
    }
    public void OnSecundary(InputValue value)
    {
        (_heldBoulderIndex, _nextBoulderIndex) = (_nextBoulderIndex, _heldBoulderIndex);
    }
    private void UpdateButtonInputs()
    {
        //OnPrimary would only be called when pressed down, not when released, thus we need to check it in update
        _primaryInput = GetComponent<PlayerInput>().actions["Primary"].IsPressed();
    }

    public void SpawnSpecialBoulder()
    {
        _nextBoulderIndex = UnityEngine.Random.Range(1, _boulderPrefabList.Count);
    }
    public int GetNextBoulderIndex() => _nextBoulderIndex;
    public int GetHeltBoulderIndex() => _heldBoulderIndex;
    public List<GameObject> GetBoulderList() => _boulderPrefabList;
}
