using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class AttackingPlayerController : PlayerController
{
    [SerializeField] private float _SprintSpeed;
    [SerializeField] private float _fallSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _cooldownSprint;
    [SerializeField] private float _DurationSprint;
    private InputAction _jump;
    private InputAction _sprint;
    private Vector3 _verticalVelocety;
    private float _timerCooldownSprint;
    private float _timerDurationSprint;
    private bool _isSprinting;

    private void Start()
    {
        _speed = _movementSpeed;
        _move = InputSystem.actions.FindAction("Move1");
        _speed = _movementSpeed;
        _timerCooldownSprint = _cooldownSprint;
        _verticalVelocety = Physics.gravity;
        _jump = InputSystem.actions.FindAction("Jump");
        _sprint = InputSystem.actions.FindAction("Sprint");
    }
    protected override void Update()
    {
        base.Update();
        ApplyGravety();
        Jump();
        sprint();
    }
    private void sprint()
    {
        _timerCooldownSprint += Time.deltaTime;
        if(_timerCooldownSprint >= _cooldownSprint && _sprint.IsPressed())
        {
            _isSprinting = true;
            _timerCooldownSprint -= _cooldownSprint;
            _speed = _SprintSpeed;
        }
        if(_isSprinting)
        {
            _timerDurationSprint += Time.deltaTime;
            if(_timerDurationSprint >= _DurationSprint)
            {
                _isSprinting = false;
                _timerDurationSprint -= _DurationSprint;
                _speed = _movementSpeed;
            }
        }
    }

    private void ApplyGravety()
    {
        _verticalVelocety = Vector3.up * Math.Clamp(_verticalVelocety.y - (_fallSpeed * Time.deltaTime),-_fallSpeed,999);
        _controller.Move(_verticalVelocety);
    }

    private void Jump()
    {
        if(_controller.isGrounded && _jump.IsPressed())
        {
            _verticalVelocety.y = _jumpSpeed;
        }
    }

}
