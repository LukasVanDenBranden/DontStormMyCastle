using System;
using UnityEngine;

public class P1Health : MonoBehaviour
{
    [SerializeField] private int _amountOfHearts;
    [SerializeField] private Material _playerMaterial;
    [SerializeField] private MeshRenderer _playerRenderer;
    public static P1Health Instance;
    private int HeartsRemaining;
    public EventHandler OnDamage;
    public AudioSource _hurtAudio;
    private Color _playerBaseColor;

    private float _invincibleDuration = 1f;
    private float _invincibleTimer;
    private void Awake()
    {
        _hurtAudio = GetComponent<AudioSource>();
        _playerMaterial = new Material(_playerMaterial); //make a instance of the material
        _playerRenderer.material = _playerMaterial;
        _playerBaseColor = _playerMaterial.color;
        Instance = this;
    }
    void Start()
    {
        HeartsRemaining = _amountOfHearts;
    }
    private void Update()
    {
        if(IsInHitStun())
        {
            _playerMaterial.color = Color.red;
        }
        else
        {
            _playerMaterial.color = _playerBaseColor;
        }
        _invincibleTimer -= Time.deltaTime;
    }
    public int GetHearths() => HeartsRemaining;
    public void takeDamage(int amount)
    {
        if (IsInHitStun()) return; 
        _invincibleTimer = _invincibleDuration;
        HeartsRemaining -= amount;
        _hurtAudio.Play();
        OnDamage?.Invoke(this,EventArgs.Empty);
    }
    public bool IsInHitStun()
    {
        return _invincibleTimer > 0;
    }
}
