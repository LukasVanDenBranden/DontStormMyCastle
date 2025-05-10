using UnityEngine;

public class P1Health : MonoBehaviour
{
    [SerializeField] private int AmountOfHearts;
    [SerializeField] private GameStateScript _gameStateScript;
    public static int HeartsRemaining;
    
    
    void Start()
    {
        HeartsRemaining = AmountOfHearts;
    }
}
