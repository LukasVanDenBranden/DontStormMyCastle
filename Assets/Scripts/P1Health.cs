using System;
using UnityEngine;

public class P1Health : MonoBehaviour
{
    [SerializeField] private int AmountOfHearts;
    public static P1Health Instance;
    private int HeartsRemaining;
    public EventHandler OnDamage;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        HeartsRemaining = AmountOfHearts;
    }
    public int GetHearths() => HeartsRemaining;
    public void takeDamage(int amount)
    {
        HeartsRemaining -= amount;
        OnDamage?.Invoke(this,EventArgs.Empty);
    }
}
