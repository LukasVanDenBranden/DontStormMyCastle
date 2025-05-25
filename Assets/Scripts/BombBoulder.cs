using UnityEngine;
using UnityEngine.UI;

public class BombBoulder : BoulderBase
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private Image _timerImage;
    private float _fuseTimer;
    private float _fuseDuration = 2f;
    protected override void Start()
    {
        base.Start();
        _fuseTimer = _fuseDuration;
    }
    protected override void Update()
    {
        _timerImage.fillAmount = _fuseTimer / _fuseDuration;
        base.Update();
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
