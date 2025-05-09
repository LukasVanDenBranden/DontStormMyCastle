using Unity.VisualScripting;
using UnityEngine;

public class P1Health : MonoBehaviour
{
    [SerializeField] private int AmountOfHearts;
    [SerializeField] private GameStateScript _gameStateScript;
    public static int HeartsRemaining;

    private static Collider collider;
    
    void Start()
    {
        HeartsRemaining = AmountOfHearts;

        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.gameObject.tag == "Finish")
        {

        }
    }

    public static bool Player1ReachesFinish()
    {
        if (collider.gameObject.tag == "Finish")
        {
            Debug.Log("yey");
            return true;
        }

        return false;
    }
}
