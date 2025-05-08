using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    private Transform _targetTransform;

    [SerializeField] bool _isTargetP1; //if true pickup is for p1, else p2
    private readonly float _attractionForce = 300f;
    private readonly float _pickupDistance = 7.5f;

    [SerializeField] int _powerupIndex;

    private void Awake()
    {
        if (_isTargetP1)
            _targetTransform = FindFirstObjectByType<P1Controller>().transform;
        else
            _targetTransform = FindFirstObjectByType<P2Controller>().transform;
    }

    private void FixedUpdate()
    {
        //pullForce = distance * -1 clamped [0, pickupdistance] then * _attractionForce
        float pullForce = Mathf.Clamp01((_pickupDistance - Vector3.Distance(_targetTransform.position, transform.position)) / _pickupDistance) * _attractionForce;
        Vector3 pullDirection = (_targetTransform.position - transform.position).normalized;

        GetComponent<Rigidbody>().AddForce(pullDirection * pullForce);

        if (Vector3.Distance(_targetTransform.position, transform.position) < 1)
            PlayerAttempsPickup(true);
    }


    public void PlayerAttempsPickup(bool isP1)
    {
        if (isP1 == true && _isTargetP1 == true)
        {
            _targetTransform.gameObject.GetComponent<P1Controller>().PickUpKey();
            Destroy(this.gameObject);
        }
        else if (isP1 == false && _isTargetP1 == false)
        {
            _targetTransform.gameObject.GetComponent<P2Controller>().SpawnSpecialBoulded(_powerupIndex);
            Destroy(this.gameObject);
        }
    }
}
