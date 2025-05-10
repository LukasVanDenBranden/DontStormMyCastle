using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] private AudioClip _explosion;
    private float _explosionDuration = 0.75f;
    private float _explosionTimer;
    private bool _hasHit;
    private void Start()
    {
        _explosionTimer = _explosionDuration;
        AudioSource.PlayClipAtPoint(_explosion, transform.position);
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
        if (!_hasHit)
        {
            P1Health.HeartsRemaining--;
        }
        _hasHit = true;
    }
}
