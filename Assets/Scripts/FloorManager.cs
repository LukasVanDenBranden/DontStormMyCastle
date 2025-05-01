using UnityEngine;

public class FloorManager : MonoBehaviour
{
    //outside connections
    [SerializeField] private GameObject _tilePrefab;

    //stats vars
    private readonly int _trackLength = 110; //total length of the moving floor
    private readonly int _trackStartX = -50; //track will go to x -50
    private readonly float _floorSpeed = 5f;

    //script vars
    private GameObject[] _floorTiles;

    private void Awake()
    {
        _floorTiles = new GameObject[_trackLength/10]; //floor tiles are 10 width, thus amount of tiles is 1 tenth the length
    }

    private void Start()
    {
        //spawn floor tiles
        for (int i = 0; i < _floorTiles.Length; i++)
        {
            Vector3 position = new Vector3(0, 0, i*10 + _trackStartX);
            _floorTiles[i] = Instantiate(_tilePrefab, position, Quaternion.identity, transform);
        }
    }

    void FixedUpdate()
    {
        UpdateFloor();
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
}
