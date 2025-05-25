using Unity.VisualScripting;
using UnityEngine;

public class FaceToCameraScript : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Start()
    {

        _cameraTransform = GameObject.Find("CameraP2").transform;
    }

    void FixedUpdate()
    {
        transform.LookAt(_cameraTransform);
    }
}
