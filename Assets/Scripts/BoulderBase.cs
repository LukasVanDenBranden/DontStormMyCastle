using Unity.VisualScripting;
using UnityEngine;

public class BoulderBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<PlayerManager>().P1Health -= 1;
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Pickup"))
        {
            collision.gameObject.GetComponent<Pickup>().PlayerAttempsPickup(false);
        }
    }
}
