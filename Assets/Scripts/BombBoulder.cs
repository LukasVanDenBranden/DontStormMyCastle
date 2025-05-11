using UnityEngine;

public class BombBoulder : BoulderBase
{
    [SerializeField] private GameObject _explosion;
    private bool _haslanded;

    private float _fuseTimer;
    private float _fuseDuration = 2;

    private void Start()
    {
        _fuseTimer = _fuseDuration;
        Debug.Log("special boulder");
    }
    private void Update()
    {
        if (!_haslanded) return;

        _fuseTimer -= Time.deltaTime;

        if(_fuseTimer <= 0)
        {
            GamepadManager.Instance.RumbleController(1, 0.2f, 0.15f);
            GamepadManager.Instance.RumbleController(2, 0.2f, 0.15f);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (_haslanded == true) return;
        _haslanded = true;
    }
}
