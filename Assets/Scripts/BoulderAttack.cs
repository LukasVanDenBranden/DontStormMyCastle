using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoulderAttack : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _boulder;
    [SerializeField] private Camera _defenderCamera;
    [SerializeField] private float _baseShootPower = 5;
    [SerializeField] private LineRenderer _shootingLine;
    [SerializeField] private Material _lineMaterial;
    private InputAction _attack;
    private bool showLine;
    private Vector3 _shootStartPointdirection;
    private Vector3 _shootEndPointdirection;
    private void Start()
    {
        _attack = InputSystem.actions.FindAction("Attack");
        _shootingLine.enabled = false;

    }
    // TODO: change this logic to include controller input and make it so that the player can choose a direction and amount of force
    void Update()
    {
        GameObject _createdBoulder;
        Rigidbody _createdBoulderRigidBody;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5;
        if (_attack.WasPressedThisFrame())
        {
            showLine = true;
            _shootStartPointdirection = _defenderCamera.ScreenToViewportPoint(mousePos);
        }
        _shootEndPointdirection = _defenderCamera.ScreenToViewportPoint(mousePos);
        Vector3 shootdirection = _shootStartPointdirection - _shootEndPointdirection;
        Vector3 shotpoint = _spawnPoint.position;
        shotpoint.z += shootdirection.y * 2;
        shotpoint.x += shootdirection.x * 2;
        Vector3 direction = (_spawnPoint.position - shotpoint) * _baseShootPower;
        if (showLine)
        {
            Vector3 linestartpoints =  _spawnPoint.position + _spawnPoint.forward * 2;
            Vector3 lineEndpoints =  shotpoint + _spawnPoint.forward * 2;
            _shootingLine.enabled = true;
            _shootingLine.material = _lineMaterial;
            _shootingLine.positionCount = 2;
            _shootingLine.SetPosition(0, linestartpoints);
            _shootingLine.SetPosition(1, lineEndpoints);
        }
        else
        {
            _shootingLine.enabled = false;
        }

        if (_attack.WasReleasedThisFrame())
        {
            showLine = false;
            _createdBoulder = Instantiate(_boulder, _spawnPoint.position, _spawnPoint.rotation);
            _createdBoulderRigidBody = _createdBoulder.GetComponent<Rigidbody>();
            if (_createdBoulderRigidBody != null)
            {
                _createdBoulderRigidBody.AddForce(direction, ForceMode.Impulse);
            }
        }
    }
}
