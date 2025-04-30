using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateTiles : MonoBehaviour
{
    [SerializeField] private GameObject _tile;
    [SerializeField] private Vector3 _startPosition;
    private List<GameObject> _listOfTiles;
    void Start()
    {
        _listOfTiles = new List<GameObject>();
        Vector3 position = GetPlacePosition();
        while (position.z < HelperClass.instance.DespawnLocation.position.z)
        {
            _listOfTiles.Add(Instantiate(_tile, GetPlacePosition(), _tile.transform.rotation));
            position = GetPlacePosition();
        }
    }

    void Update()
    {
        for (int i = _listOfTiles.Count() - 1; i >= 0; i--)
        {
            if (_listOfTiles[i].transform.position.z < HelperClass.instance.DespawnLocation.position.z)
            {
                Destroy(_listOfTiles[i]);
                _listOfTiles.RemoveAt(i);
                _listOfTiles.Add(Instantiate(_tile, GetPlacePosition(), _tile.transform.rotation));
            }
        }

    }
    private Vector3 GetPlacePosition()
    {
        Vector3 placeposition = Vector3.zero;
        if (_listOfTiles.Count <= 0)
        {
            placeposition = _startPosition;
            return placeposition;
        }
        placeposition.z = _listOfTiles.Max(z => z.transform.position.z) + _tile.transform.localScale.y - 0.001f;
        return placeposition;
    }
}
