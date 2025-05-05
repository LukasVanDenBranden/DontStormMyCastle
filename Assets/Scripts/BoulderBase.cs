using Unity.VisualScripting;
using UnityEngine;

public class BoulderBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
            P1Health.HeartsRemaining -= 1;
            Debug.Log(P1Health.HeartsRemaining);

            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Pickup")
        {
            collision.gameObject.GetComponent<Pickup>().PlayerAttempsPickup(false);
        }
    }
}
