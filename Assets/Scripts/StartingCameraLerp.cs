using System.Threading;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class StartingCameraLerp : MonoBehaviour
{
    private Transform _startpoint;
    [SerializeField] private Transform _endpoint;
    [SerializeField] Camera _camera;
    private float _timer;
    [SerializeField] private float _timerDuration;
    [SerializeField] private bool _isLeftSide;
    [SerializeField] private float _startingDelay = 1;
    private void Start()
    {
        _startpoint = GameObject.Find("CameraStartPosition").transform;
        _timer = -_startingDelay;
    }
    void Update()
    {
        if (_timer > _timerDuration)
        {
            Destroy(this);
            return;
        }
        _camera.transform.position = Vector3.Lerp(_startpoint.position, _endpoint.position, _timer); 
        _camera.transform.rotation = Quaternion.Lerp(_startpoint.rotation, _endpoint.rotation, _timer);
        float splitAmount = Mathf.Lerp(0f, 0.5f, _timer);
        if (_isLeftSide)
        {
            _camera.rect = new Rect(0f, 0f, splitAmount, 1f);
        }
        else
        {
            _camera.rect = new Rect(0f + splitAmount, 0f, 1 - splitAmount, 1f);
        }
        _timer += Time.deltaTime / _timerDuration;
    }
}
