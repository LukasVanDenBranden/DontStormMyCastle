using UnityEngine;

public class SlowlingField : MonoBehaviour
{
    [SerializeField] private float speedreduction = 0.5f;
    private float _timeuntilDeletion = 3f;
    private float _deletionTimer;
    private bool _hasHit;
    private void Start()
    {
        _deletionTimer = _timeuntilDeletion;
    }
    private void Update()
    {
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
            P1Controller.Instance.SetMoveSpeedMultiplier(speedreduction);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        P1Controller.Instance.SetMoveSpeedMultiplier(1f);
    }
}
