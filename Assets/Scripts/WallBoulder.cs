using UnityEngine;

public class WallBoulder : BoulderBase
{
    private Rigidbody _rb;
    private FloorManager _floorManager;

    private bool _haslanded;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _floorManager = FindFirstObjectByType<FloorManager>();
    }
    private void Update()
    {
        if (!_haslanded) return;
        _rb.linearVelocity = new Vector3(0, 0, -_floorManager.GetFloorSpeed());

    }
    protected override void OnCollisionEnter(Collision collision)
    {
        _haslanded = true;
    }
}
