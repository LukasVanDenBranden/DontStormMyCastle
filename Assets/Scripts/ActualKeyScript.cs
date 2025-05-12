using Unity.VisualScripting;
using UnityEngine;

public class ActualKeyScript : MonoBehaviour
{
    private FloorManager _floorManager;
    private float _keySpawnTimer;
    private P1Controller p1Controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _floorManager = FindFirstObjectByType<FloorManager>();
        p1Controller = FindFirstObjectByType<P1Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _floorManager.GetFloorSpeed() * Time.deltaTime * -Vector3.forward;

        if (transform.position.z <= -80) Destroy(gameObject);
    }

}


