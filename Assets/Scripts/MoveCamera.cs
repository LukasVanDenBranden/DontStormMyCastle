using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private string _tag = "RunningCamera";
    [SerializeField] private Rect _viewRectangle = new Rect(0, 0, 0.5f, 1);
    private void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag(_tag).GetComponent<Camera>();
    }
    void Start()
    {
        _camera.rect = _viewRectangle;
    }
    void Update()
    {
        _camera.transform.position = transform.position;
        _camera.transform.rotation = transform.rotation;
    }
}
