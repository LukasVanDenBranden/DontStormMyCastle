using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected Data _data;
    [SerializeField] protected CharacterController _controller;
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected Transform _body;
    protected InputAction _move;
    protected float _speed;
    public event EventHandler OnPlayerDestroy;

    protected virtual void Update()
    {
        MoveWithInPut();
    }

    protected virtual void MoveWithInPut()
    {
        Vector2 movementInputValues = _move.ReadValue<Vector2>();

        if (movementInputValues != null) LookInMovementDirection(movementInputValues);

        _controller.Move(transform.right * movementInputValues.x * _speed * Time.deltaTime);
        _controller.Move(transform.forward * movementInputValues.y * _speed * Time.deltaTime);
    }
    private void LookInMovementDirection(Vector2 movementInputValues)
    {
        Vector3 lookdirection = new Vector3(movementInputValues.x, 0, movementInputValues.y);
        _body.transform.localRotation = Quaternion.LookRotation((lookdirection).normalized);
    }
    private void OnDestroy()
    {
        OnPlayerDestroy?.Invoke(this, EventArgs.Empty);
    }
}
