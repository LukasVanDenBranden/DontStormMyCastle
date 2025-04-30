using UnityEngine;

public class HelperClass : MonoBehaviour
{
    public static HelperClass instance;
    [SerializeField] public Transform DespawnLocation;


    public int test = 5;
    public float WorldMoveSpeed = 10;


    private void Awake()
    {
        instance = this;
    }
    public bool testFunctie()
    {
        return true;
    }
}
