using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1Controller : MonoBehaviour
{
    //outside connections
    private Rigidbody _rb;
    private Camera _Camera;

    //stats vars
    private readonly float _moveSpeed = 1000f;

    //script vars
    private Vector2 _moveInput;
    private Vector2 _rotateInput;
    private bool _primaryInput = false;

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
        _rb.linearVelocity = new Vector3(_moveInput.x, 0, _moveInput.y) * _moveSpeed * Time.fixedDeltaTime;
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
}
