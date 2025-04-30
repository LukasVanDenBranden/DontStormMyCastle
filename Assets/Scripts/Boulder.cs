using UnityEngine;

public class Boulder : MonoBehaviour
{
    //private bool _hasLanded;
    //private Rigidbody _rigidbody;
    //private SphereCollider _colider;
    //private void Start()
    //{
    //    TryGetComponent<Rigidbody>(out _rigidbody);
    //    TryGetComponent<SphereCollider>(out _colider);
    //}
    //private void Update()
    //{
    //    if (!_hasLanded) return;
    //    transform.position -= Vector3.forward * HelperClass.instance.WorldMoveSpeed * Time.deltaTime;
    //    if (transform.position.z < HelperClass.instance.DespawnLocation.position.z)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    CheckIfCollidedWithPlayer(collision.gameObject);
    //    _colider.isTrigger = true;
    //    _hasLanded = true;
    //    _rigidbody.isKinematic = true;
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    CheckIfCollidedWithPlayer(other.gameObject);
    //}
    //
    //private void CheckIfCollidedWithPlayer(GameObject collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (!TryGetComponent<DefendingPlayerController>(out DefendingPlayerController script))
    //        {
    //            Destroy(collision.gameObject);
    //        }
    //    }
    //}

}
