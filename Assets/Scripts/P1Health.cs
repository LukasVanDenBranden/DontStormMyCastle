using UnityEngine;

public class P1Health : MonoBehaviour
{
    [SerializeField] private int AmountOfHearts;
    public static int HeartsRemaining;
    
    
    void Start()
    {
        HeartsRemaining = AmountOfHearts;
    }

    // Update is called once per frame
    void Update()
    {
        if (HeartsRemaining <= 0)
        {
            Debug.Log("u ded");
            Time.timeScale = 0;

        }
    }
}
