using UnityEngine;
using UnityEngine.InputSystem;

public class P2Controller : MonoBehaviour
{
    //outside connections
    private Rigidbody _rb;

    //stats vars
    private int _maxMoveRange = 22;
    private float _moveSpeed = 1000f;

    //script vars
    private Vector2 moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        _rb.linearVelocity = new Vector3(-moveInput.x * _moveSpeed * Time.fixedDeltaTime, 0, 0);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
