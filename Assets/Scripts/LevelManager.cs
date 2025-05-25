using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //shit like points or in game events will be managed here

    [SerializeField] private List<GameObject> _pickupPrefabsP1;
    [SerializeField] private List<GameObject> _pickupPrefabsP2;
    [SerializeField] private List<GameObject> _obstaclePrefabs;
    private List<GameObject> _obstacleList;
    [SerializeField] private GameObject _keyPrefab;
    private FloorManager _floorManager;
    public static LevelManager Instance;

    //stats
    private float _pickupsP2SpawnTime = 7.5f; //in seconds
    private float _obstacleSpawnTime = 0.5f; //in seconds
    public float _keySpawnTime = 5f; //in seconds
    private float _trackWidth = 16f;

    //script vars
    private float _pickupP1Timer = 5;
    private float _pickupP2Timer = 5;
    [SerializeField] private float _obstacleTimer = 0.5f;
    private float _keySpawnTimer = 5f;
    private Vector3 _pickupP2Place = new Vector3(0, 1, -25);
    private float _obstacleSpawnZ = 50f;
    private Transform _pickUpSpawnpoint;
    private void Start()
    {
        Instance = this;
        _obstacleList = new List<GameObject>();
        _pickUpSpawnpoint = GameObject.Find("PickUpSpawnPoint").transform;
        _floorManager = FindFirstObjectByType<FloorManager>();
    }

    private void FixedUpdate()
    {
        PlacePickups();

        PlaceObstacles();

        PlaceKeys();
    }

    private void PlacePickups()
    {
        if (GameStateScript.Instance.StartTimer > 0) return;
        //place pickups for P2
        _pickupP1Timer -= Time.fixedDeltaTime;
        _pickupP2Timer -= Time.fixedDeltaTime;

        if (_pickupP2Timer < 0)
        {
            _pickupP2Place = _pickUpSpawnpoint.position;
            _pickupP2Place.x = Random.Range(-_trackWidth, _trackWidth);
            Instantiate(_pickupPrefabsP2[Random.Range(0, _pickupPrefabsP2.Count)], _pickupP2Place, Quaternion.identity);
            _pickupP2Timer += _pickupsP2SpawnTime - _floorManager.GetFloorSpeed() / 10;
        }
    }

    private void PlaceObstacles()
    {
        if (GameStateScript.Instance.StartTimer > 0) return;
        _obstacleTimer -= Time.fixedDeltaTime;

        if (_obstacleTimer < 0)
        {
            Vector3 obstacleSpawnPosition = new Vector3(Random.Range(-_trackWidth, _trackWidth), 0, _obstacleSpawnZ);
            _obstacleList.Add(Instantiate(_obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)], obstacleSpawnPosition, Quaternion.identity));
            _obstacleTimer += _obstacleSpawnTime - _floorManager.GetFloorSpeed() / 100;
        }
    }

    private void PlaceKeys()
    {
        if (GameStateScript.Instance.StartTimer > 0) return;

        _keySpawnTimer -= Time.fixedDeltaTime;

        if (_keySpawnTimer < 0)
        {
            Vector3 keySpawnPosition = Vector3.zero;
            do
            {
                keySpawnPosition = new Vector3(Random.Range(-_trackWidth, _trackWidth), 5, _obstacleSpawnZ);
            }
            while (_obstacleList.Any(o => o.GetComponent<Collider>().bounds.Contains(keySpawnPosition)));
            GameObject key = Instantiate(_keyPrefab, keySpawnPosition, Quaternion.identity);
            _keySpawnTimer += _keySpawnTime - _floorManager.GetFloorSpeed() / 100;
        }
    }
    public void removeObstacleFromList(GameObject obstacle)
    {
        _obstacleList.Remove(obstacle);
    }
    public float GetTrackWidth()
    {
        return _trackWidth;
    }
}
