using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //shit like points or in game events will be managed here

    [SerializeField] private List<GameObject> _pickupPrefabsP1;
    [SerializeField] private List<GameObject> _pickupPrefabsP2;
    [SerializeField] private List<GameObject> _obstaclePrefabs;
    private FloorManager _floorManager;

    //stats
    private float _pickupsP1SpawnTime = 10; //in seconds
    private float _pickupsP2SpawnTime = 10; //in seconds
    private float _obstacleSpawnTime = 0.5f; //in seconds
    private float _trackWidth = 20f;

    //script vars
    private float _pickupP1Timer = 5;
    private float _pickupP2Timer = 5;
    private float _obstacleTimer = 0.5f;
    private Vector3 _pickupP2Place = new Vector3(0, 1, -25);
    private float _obstacleSpawnZ = 50f;

    private void Start()
    {
        _floorManager = FindFirstObjectByType<FloorManager>();
    }

    private void FixedUpdate()
    {
        PlacePickups();
        PlaceObstacles();
    }

    private void PlacePickups()
    {
        //place pickups for P2
        _pickupP1Timer -= Time.fixedDeltaTime;
        _pickupP2Timer -= Time.fixedDeltaTime;

        if (_pickupP2Timer < 0)
        {
            _pickupP2Place.x = Random.Range(-_trackWidth, _trackWidth);
            Instantiate(_pickupPrefabsP2[Random.Range(0, _pickupPrefabsP2.Count)], _pickupP2Place, Quaternion.identity);
            _pickupP2Timer += _pickupsP2SpawnTime - _floorManager.GetFloorSpeed()/10;
        }
    }

    private void PlaceObstacles()
    {
        _obstacleTimer -= Time.fixedDeltaTime;

        if (_obstacleTimer < 0)
        {
            Vector3 obstacleSpawnPosition = new Vector3(Random.Range(-_trackWidth, _trackWidth), 0, _obstacleSpawnZ);
            Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)], obstacleSpawnPosition, Quaternion.identity);
            _obstacleTimer += _obstacleSpawnTime - _floorManager.GetFloorSpeed() / 100;
        }
    }
}
