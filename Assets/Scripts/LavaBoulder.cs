using UnityEngine;

public class LavaBoulder : BoulderBase
{
    [SerializeField] private GameObject _lavaPool;

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Vector3 placePosition = transform.position;
            placePosition.y = _lavaPool.transform.position.y;
            Instantiate(_lavaPool, placePosition, _lavaPool.transform.rotation);
            Destroy(gameObject);
        }
    }
}
