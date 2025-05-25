using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    protected Transform _targetTransform;

    [SerializeField] protected bool _isForP1; //if true pickup is for p1, else p2

    private void Awake()
    {
        if (_isForP1)
        {
            _targetTransform = FindFirstObjectByType<P1Controller>().transform;
        }
        else
        {
            _targetTransform = FindFirstObjectByType<P2Controller>().transform;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; //freeze
        }
    }

    private void FixedUpdate()
    {
        //pullForce = distance * -1 clamped [0, pickupdistance] then * _attractionForce
        //float pullForce = Mathf.Clamp01((_pickupDistance - Vector3.Distance(_targetTransform.position, transform.position)) / _pickupDistance) * _attractionForce;
        //Vector3 pullDirection = (_targetTransform.position - transform.position).normalized;

        //GetComponent<Rigidbody>().AddForce(pullDirection * pullForce);

        //if close enough pick up
        if (Vector3.Distance(_targetTransform.position, transform.position) < 2.5f)
            PlayerAttempsPickup(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerAttempsPickup(_isForP1);
    }

    public virtual void PlayerAttempsPickup(bool isP1)
    {
        if(isP1)
        {
            if(_isForP1)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (_isForP1)
            {
                Destroy(gameObject);
            }
            else
            {
                _targetTransform.gameObject.GetComponent<P2Controller>().SpawnSpecialBoulder();
                Destroy(gameObject);
            }
        }
    }
}
