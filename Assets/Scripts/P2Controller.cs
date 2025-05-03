using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : MonoBehaviour
{
    //outside connections
    private Rigidbody _rb;
    private Camera _Camera;
    [SerializeField] private GameObject _boulderPrefab;
    private List<GameObject> _boulderList;
    private RectTransform _primaryChargeUI;

    //stats vars
    private readonly float _moveSpeed = 1000f;
    private readonly float _rotationSpeed = 50f;
    private readonly float _maxRotation = 50f;
    private readonly float _primaryMaxThrowForce = 30f;
    private readonly float _primaryThrowTime = 3f; //time it takes to reach max throwing force in seconds
    private readonly float _despawnYLevel = -2;
    private float _boulderCooldown = 0.75f;
    private float _boulderCooldownTimer;
    //script vars
    private Vector2 moveInput;
    private Vector2 _rotateInput;
    private bool _primaryInput = false;
    private float _primaryThrowForce = 0f;
    private GameObject _currentThrowingBoulder;

    private void Awake()
    {
        _boulderList = new List<GameObject>();
        _boulderCooldownTimer = _boulderCooldown;
        _rb = GetComponent<Rigidbody>();
        _primaryChargeUI = GameObject.Find("PrimaryForceCharge").GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        UpdateButtonInputs();

        UpdateMovement();
        UpdatePrimary();

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

    private void UpdateUI()
    {
        //temp magic value (height/2 so it starts at bottom)
        _primaryChargeUI.anchoredPosition = new Vector3(0, 23.75f * (_primaryThrowForce / _primaryMaxThrowForce) - 23.75f, 0);
        _primaryChargeUI.sizeDelta = new Vector2(_primaryChargeUI.sizeDelta.x, 47.5f * (_primaryThrowForce / _primaryMaxThrowForce));
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
