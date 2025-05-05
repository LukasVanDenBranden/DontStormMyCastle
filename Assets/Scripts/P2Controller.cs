using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : MonoBehaviour
{
    //outside connections
    private Rigidbody _rb;
    private Camera _camera;
    private RectTransform _primaryChargeUI;
    [SerializeField] private GameObject _boulderPrefab;
    private List<GameObject> _boulderList;

    //stats vars
    private readonly float _moveSpeed = 1000f;
    private readonly float _rotationSpeed = 50f;
    private readonly float _maxRotation = 50f;
    private readonly float _primaryMaxThrowForce = 30f;
    private readonly float _primaryThrowTime = 3f; //time it takes to reach max throwing force in seconds
    private readonly float _despawnYLevel = -10f;
    private readonly float _boulderCooldown = 0.75f;
    private readonly float _cameraDollyZoomStrength = 5f;
    //script vars
    private Vector2 moveInput;
    private Vector2 _rotateInput;
    private bool _primaryInput = false;
    private float _primaryThrowForce = 0f;
    private GameObject _currentThrowingBoulder;
    private float _boulderCooldownTimer;
    private float _cameraDefaultDistance;
    private float _cameraDefaultFOV;
    private float _chargePercentage = 0f; //[0, 1] to how much is charged AND goes slowely down using cooldown

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>();
        _primaryChargeUI = GameObject.Find("PrimaryForceCharge").GetComponent<RectTransform>();
        _boulderList = new List<GameObject>();
    }

    private void Start()
    {
        _boulderCooldownTimer = _boulderCooldown;
        _cameraDefaultDistance = _camera.transform.localPosition.z;
        _cameraDefaultFOV = _camera.fieldOfView;
    }

    private void FixedUpdate()
    {
        UpdateButtonInputs();

        UpdateMovement();
        UpdatePrimary();

        //update percentage
        _chargePercentage = Mathf.MoveTowards(_chargePercentage, _primaryThrowForce / _primaryMaxThrowForce, 10 * Time.fixedDeltaTime);

        UpdateCamera();
        UpdateUI();
        CleanUpBoulders();
    }

    private void UpdateMovement()
    {
        //add forces by input
        _rb.linearVelocity = new Vector3(-moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, 0);
        _rb.angularVelocity = new Vector3(0, _rotateInput.x * _rotationSpeed * Time.fixedDeltaTime, 0);
        //apply max rotation angle
        float yRotation = Mathf.Clamp(transform.eulerAngles.y, 180 - _maxRotation, 180 + _maxRotation);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
    }
    private void UpdatePrimary()
    {
        _boulderCooldownTimer -= Time.deltaTime;
        if (_boulderCooldownTimer >= 0) return;

        if (!_primaryInput)
        {
            //was throwing and now released, thus throw the boulder
            if (_currentThrowingBoulder != null)
            {
                _currentThrowingBoulder.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * _primaryThrowForce, ForceMode.Impulse); //add force with same direction player is looking
                //reset for next boulder
                _currentThrowingBoulder = null;
                _primaryThrowForce = 0f;
                _boulderCooldownTimer = _boulderCooldown;
            }
            return;
        }

        //in front of player with the magic number being the distance from player
        Vector3 boulderPos = transform.position + transform.forward.normalized * 7.5f;

        //if no boulder (first loop when button is pressed) spawn boulder, otherwise update its position
        if (_currentThrowingBoulder == null)
            _currentThrowingBoulder = Instantiate(_boulderPrefab, boulderPos, Quaternion.identity);
        else
            _currentThrowingBoulder.transform.position = boulderPos;
        _boulderList.Add(_currentThrowingBoulder);
        _currentThrowingBoulder.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;//stop boulder from accumilating falling speed
        //charge throw force
        _primaryThrowForce += _primaryMaxThrowForce * (Time.fixedDeltaTime / _primaryThrowTime);
        //clamp throwforce
        if (_primaryThrowForce > _primaryMaxThrowForce)
            _primaryThrowForce = _primaryMaxThrowForce;
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
            if (_boulderList[i].transform.position.y < _despawnYLevel)
            {
                Destroy(_boulderList[i]);
                _boulderList.RemoveAt(i);
            }
        }
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
    private void UpdateButtonInputs()
    {
        //OnPrimary would only be called when pressed down, not when released, thus we need to check it in update
        _primaryInput = GetComponent<PlayerInput>().actions["Primary"].IsPressed();
    }
}
