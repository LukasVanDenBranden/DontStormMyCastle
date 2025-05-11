using UnityEngine;

public class WallBoulder : BoulderBase
{
    [SerializeField] private GameObject _obstacle;

    protected override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            Instantiate(_obstacle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
