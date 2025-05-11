using Unity.VisualScripting;
using UnityEngine;

public class BoulderBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
        Debug.Log("boulder hit player");
            P1Health.Instance.takeDamage(1);
            GamepadManager.Instance.RumbleController(1, 0.3f, 0.1f);
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Pickup"))
        {
            Debug.Log("boulder hit pickup");
            collision.gameObject.GetComponent<Pickup>().PlayerAttempsPickup(false);
        }
    }
}
