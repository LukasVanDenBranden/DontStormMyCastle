using System.Runtime.CompilerServices;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject Key;
    private LevelManager levelManager;
    private float trackWidth;
    private float _keySpawnTime = 5;
    private float _keySpawnTimer = 3;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
        trackWidth = levelManager.GetTrackWidth();
    }

    // Update is called once per frame
    void Update()
    {
        _keySpawnTimer += Time.deltaTime;

        if (_keySpawnTimer >= _keySpawnTime)
        {
            SpawnKey();
            _keySpawnTimer = 0;
        }
    }

    private void SpawnKey()
    {
        GameObject key = Instantiate(Key, MakeKeyStartPosition(), Quaternion.identity);
    }

    private Vector3 MakeKeyStartPosition()
    {
        float randomXvalue = Random.Range(-trackWidth, trackWidth);
        return new Vector3(randomXvalue, 3, 50);
    }
}
