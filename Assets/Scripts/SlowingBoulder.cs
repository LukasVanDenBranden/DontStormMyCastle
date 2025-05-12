using UnityEngine;

public class SlowingBoulder : BoulderBase
{
    [SerializeField] private GameObject _slowingPlane;
    private Vector3 _lastPlacedSlowingField;

    protected override void Update()
    {
        base.Update();
        if (!_hasLanded) return;
        float distance = (transform.position - _lastPlacedSlowingField).magnitude;
        if (distance > _slowingPlane.transform.localScale.x / 2)
        {
            CreateNewSlowingField();
        }
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (_hasLanded) return;
        CreateNewSlowingField();
    }

    private void CreateNewSlowingField()
    {
        _lastPlacedSlowingField = transform.position;
        Vector3 instantiatePosition = _lastPlacedSlowingField;
        instantiatePosition.y = _slowingPlane.transform.position.y;
        Instantiate(_slowingPlane, instantiatePosition, _slowingPlane.transform.rotation);
    }
}
