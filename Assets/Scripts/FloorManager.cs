using UnityEngine;

public class FloorManager : MonoBehaviour
{
    //outside connections
    [SerializeField] private GameObject _tilePrefab;

    //stats vars
    private readonly int _trackLength = 110; //total length of the moving floor
    private readonly int _trackStartX = -50; //track will go to x -50
    private static float _floorSpeed = 15f;
    private float _maxFloorSpeed = 45f;

    private int _tileWidth = 5;

    //script vars
    private GameObject[] _floorTiles;

    private void Awake()
    {
        _floorSpeed = 15;
        _floorTiles = new GameObject[_trackLength/ _tileWidth]; //floor tiles are 10 width, thus amount of tiles is 1 tenth the length
    }

    private void Start()
    {
        //spawn floor tiles
        for (int i = 0; i < _floorTiles.Length; i++)
        {
            Vector3 position = new Vector3(0, 0, i * _tileWidth + _trackStartX);
            _floorTiles[i] = Instantiate(_tilePrefab, position, Quaternion.identity, transform);
        }
    }

    void FixedUpdate()
    {
        UpdateFloor();
        //make floor faster
        _floorSpeed += Time.fixedDeltaTime / 10;
        _floorSpeed = Mathf.Clamp(_floorSpeed, 0, _maxFloorSpeed);
    }

    void UpdateFloor()
    {
        //move tiles
        foreach (GameObject tile in _floorTiles)
        {
            tile.transform.position += Vector3.back * _floorSpeed * Time.fixedDeltaTime;
            if (tile.transform.position.z < _trackStartX)
                tile.transform.position += new Vector3(0, 0, _trackLength); //reset tile to the back once needed
        }
    }

    public float GetFloorSpeed()
    {
        return _floorSpeed;
    }
}
