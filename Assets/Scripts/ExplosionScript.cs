using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private float _explosionDuration = 0.75f;
    private float _explosionTimer;
    private void Start()
    {
        _explosionTimer = _explosionDuration;
    }
    private void Update()
    {
        _explosionTimer -= Time.deltaTime;
        if (_explosionTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player hit");
        Destroy(gameObject);
    }
}
