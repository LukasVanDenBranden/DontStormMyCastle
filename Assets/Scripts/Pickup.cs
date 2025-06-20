using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public class Pickup : MonoBehaviour
{
    protected Transform _targetTransform;

    [SerializeField] protected bool _isForP1; //if true pickup is for p1, else p2
    [SerializeField] private Material[] _materials; //index 0 is never chosen for a pickup
    [SerializeField] private Renderer _ImageRenderer;

    private int powerupIndex = 0;

    private void Awake()
    {
        if (_isForP1)
        {
            _targetTransform = FindFirstObjectByType<P1Controller>().transform;
        }
        else
        {
            _targetTransform = FindFirstObjectByType<P2Controller>().transform;
        }
    }

    private void Start()
    {
        if(!_isForP1)
        {
            powerupIndex = Random.Range(1, _targetTransform.gameObject.GetComponent<P2Controller>()._boulderPrefabList.Count);
            _ImageRenderer.material = _materials[powerupIndex];
        }
    }

    private void FixedUpdate()
    {
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
                _targetTransform.gameObject.GetComponent<P2Controller>().SpawnSpecialBoulder(powerupIndex);
                Destroy(gameObject);
            }
        }
    }
}
