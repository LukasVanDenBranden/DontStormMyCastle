using Unity.VisualScripting;
using UnityEngine;

public class BoulderBase : MonoBehaviour
{
    [SerializeField] private Sprite _boulderIcon;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            P1Health.Instance.takeDamage(1);
            GamepadManager.Instance.RumbleController(1, 0.3f, 0.1f);
            //Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Pickup"))
        {
            collision.gameObject.GetComponent<Pickup>().PlayerAttempsPickup(false);
        }
    }
    public Sprite GetBoulderIcon() => _boulderIcon;
}
