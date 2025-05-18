using Unity.VisualScripting;
using UnityEngine;

public class BoulderBase : MonoBehaviour
{
    [SerializeField] private Sprite _boulderIcon;
    protected bool _hasLanded;
    private Rigidbody _rb;
    private FloorManager _floorManager;
    protected virtual void Start()
    {
        _floorManager = FindAnyObjectByType<FloorManager>();
        _rb = GetComponent<Rigidbody>();
    }
    protected virtual void Update()
    {
        if (!_hasLanded) return;

        if (_rb.linearVelocity.z < _floorManager.GetFloorSpeed())
            _rb.AddForce(Vector3.back * _floorManager.GetFloorSpeed() /5, ForceMode.Acceleration);
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            _hasLanded = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            P1Health.Instance.takeDamage(1);
            GamepadManager.Instance.RumbleController(1, 0.3f, 0.1f);
            //Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Pickup"))
        {
            collision.gameObject.GetComponent<Pickup>().PlayerAttempsPickup(false);
        }
    }
    public Sprite GetBoulderIcon() => _boulderIcon;
}
