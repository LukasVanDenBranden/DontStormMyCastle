using Unity.VisualScripting;
using UnityEngine;

public class BoulderBase : MonoBehaviour
{
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
        }
    }
}
