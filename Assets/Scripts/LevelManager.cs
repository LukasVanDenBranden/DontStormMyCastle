using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //shit like points or in game events will be managed here

    [SerializeField] private List<GameObject> _pickupPrefabsP1;
    [SerializeField] private List<GameObject> _pickupPrefabsP2;

    private float tempPickupTime = 10; //in seconds
    private float tempPickupTimer = 5;
    private Vector3 tempSpawnPosition = new Vector3(0, 1, -25);

    private void FixedUpdate()
    {
        //TODO: make a decent solution of where to place boulders

        tempPickupTimer -= Time.fixedDeltaTime;

        if(tempPickupTimer < 0 )
        {
            tempSpawnPosition.x = Random.Range(-20, 20);
            Instantiate(_pickupPrefabsP2[Random.Range(0, _pickupPrefabsP2.Count)], tempSpawnPosition, Quaternion.identity);
            tempPickupTimer += tempPickupTime;
        }
    }
}
