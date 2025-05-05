using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    private Transform _targetTransform;

    [SerializeField] bool _isTargetP1; //if true pickup is for p1, else p2
    private readonly float _attractionForce = 300f;
    private readonly float _pickupDistance = 7.5f;

    private void FixedUpdate()
    {
        if (_targetTransform == null)
        {
            FindTarget();
            return;
        }

        //pullForce = distance * -1 clamped [0, pickupdistance] then * _attractionForce
        float pullForce = Mathf.Clamp01((_pickupDistance - Vector3.Distance(_targetTransform.position, transform.position)) / _pickupDistance) * _attractionForce;
        Vector3 pullDirection = (_targetTransform.position - transform.position).normalized;

        GetComponent<Rigidbody>().AddForce(pullDirection * pullForce);

        if(Vector3.Distance(_targetTransform.position, transform.position) < 1)
            Destroy(this.gameObject);
    }

    private void FindTarget()
    {
        if (_isTargetP1)
            _targetTransform = FindFirstObjectByType<P1Controller>().transform;
        else
            _targetTransform = FindFirstObjectByType<P2Controller>().transform;
    }
}
