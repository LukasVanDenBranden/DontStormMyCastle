using UnityEngine;
using UnityEngine.InputSystem;
public class DefendingPlayerController : PlayerController
{
    private void Start()
    {
        _speed = _movementSpeed;
        _move = InputSystem.actions.FindAction("Move2");
    }
    protected override void MoveWithInPut()
    {
        Vector2 movementInputValues = _move.ReadValue<Vector2>();

        if(movementInputValues != null) LookInMovementDirection(movementInputValues);
        _controller.Move(transform.right * movementInputValues.x * _movementSpeed * Time.deltaTime);
    }
    private void LookInMovementDirection(Vector2 movementInputValues)
    {
        Vector3 lookdirection = new Vector3(movementInputValues.x, 0, movementInputValues.y);
        _body.transform.localRotation = Quaternion.LookRotation((lookdirection).normalized);
    }
}
