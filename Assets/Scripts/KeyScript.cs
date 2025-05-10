using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Key;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnKey();
    }

    private void SpawnKey()
    {
        GameObject key = Instantiate(Key, Vector3.forward * 50, Quaternion.identity);
    }
}
