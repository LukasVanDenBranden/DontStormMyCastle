using UnityEngine;

public class KeyPickup : Pickup
{
    [SerializeField] private GameObject _keyBurstParticles;
    [SerializeField] private AudioClip _pickUpSound;
    public override void PlayerAttempsPickup(bool isP1)
    {
        if (isP1)
        {
            AudioSource.PlayClipAtPoint(_pickUpSound,GameObject.FindGameObjectWithTag("AttackingCamera").transform.position,5f);
            Instantiate(_keyBurstParticles,transform.position,Quaternion.identity);
            _targetTransform.gameObject.GetComponent<P1Controller>().PickUpKey();
            Destroy(gameObject);
        }
    }
}
