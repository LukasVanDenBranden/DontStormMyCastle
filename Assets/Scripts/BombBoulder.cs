using UnityEngine;

public class BombBoulder : BoulderBase
{
    [SerializeField] private GameObject _explosion;

    private float _fuseTimer;
    private float _fuseDuration = 2;

    protected override void Start()
    {
        base.Start();
        _fuseTimer = _fuseDuration;
    }
    protected override void Update()
    {
        base.Update();
        if (!_hasLanded) return;

        _fuseTimer -= Time.deltaTime;

        if (_fuseTimer <= 0)
        {
            GamepadManager.Instance.RumbleController(1, 0.2f, 0.15f);
            GamepadManager.Instance.RumbleController(2, 0.2f, 0.15f);
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
