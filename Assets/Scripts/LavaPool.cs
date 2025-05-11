using UnityEngine;

public class LavaPool : MonoBehaviour
{
    private float _timeuntilDeletion = 8f;
    private float _deletionTimer;
    private FloorManager _floorManager;

    private void Start()
    {
        _deletionTimer = _timeuntilDeletion;
        _floorManager = FindFirstObjectByType<FloorManager>();
    }
    private void Update()
    {
        transform.position += Vector3.back *_floorManager.GetFloorSpeed() * Time.deltaTime;
        _deletionTimer -= Time.deltaTime;
        if (_deletionTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            P1Health.Instance.takeDamage(1);
        }
    }
}
