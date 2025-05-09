using Unity.VisualScripting;
using UnityEngine;

public class P1Health : MonoBehaviour
{
    [SerializeField] private int AmountOfHearts;
    [SerializeField] private GameStateScript _gameStateScript;
    public static int HeartsRemaining;

    private static Collider collides;
    
    void Start()
    {
        HeartsRemaining = AmountOfHearts;

        collides = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collides.gameObject.CompareTag("Finish"))
        {

        }
    }

    public static bool Player1ReachesFinish()
    {
        if (collides.gameObject.CompareTag("Finish"))
        {
            Debug.Log("yey");
            return true;
        }

        return false;
    }
}
