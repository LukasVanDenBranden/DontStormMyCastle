using UnityEngine;
using UnityEngine.InputSystem;

public class TrowDirectionIndicator : MonoBehaviour
{
    void Update()
    {
        
    }
    public void OnRotate(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }
}
