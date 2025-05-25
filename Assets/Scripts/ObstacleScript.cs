using UnityEngine;
using UnityEngine.Rendering;

public class ObstacleScript : MonoBehaviour
{
    private Transform _transform;
    private FloorManager _floorManager;

    //stat vars
    private readonly float _popupTime = 0.25f; //time it takes to get full size

    //script vars
    private Vector3 _baseScale; //scale set at start
    private bool _isSpawning = true;
    private float _timeSinceSpawn = 0f;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _floorManager = FindFirstObjectByType<FloorManager>();
    }

    void Start()
    {
        _baseScale = transform.localScale;
        transform.localScale *= 0.01f;
    }

    private void FixedUpdate()
    {
        _timeSinceSpawn += Time.fixedDeltaTime;

        UpdateScale();
        UpdateMovement();

        if(transform.position.z < -75)
            Destroy(gameObject);
    }

    private void UpdateScale()
    {
        if (_isSpawning)
        {
            //get percentage of animation
            float t = Mathf.Clamp01((_timeSinceSpawn / _popupTime));//* (1 / _popupTime));
            transform.localScale = Vector3.Lerp(_baseScale * 0.01f, _baseScale, t);

            //if popup time is over, stop animation
            if (t >= _popupTime)
            {
                transform.localScale = _baseScale;
                _isSpawning = false;
            }
        }
    }

    private void UpdateMovement()
    {
        _transform.position += new Vector3(0, 0, -_floorManager.GetFloorSpeed() * Time.fixedDeltaTime);
    }
    private void OnDestroy()
    {
        LevelManager.Instance.removeObstacleFromList(gameObject);
    }
}
