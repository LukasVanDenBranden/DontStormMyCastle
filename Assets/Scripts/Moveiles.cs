using UnityEngine;

public class Moveiles : MonoBehaviour
{
    [SerializeField] private Data _data;
    void Update()
    {
        transform.position -= Vector3.forward * HelperClass.instance.WorldMoveSpeed * Time.deltaTime;
    }
}
