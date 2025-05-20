using System;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour {
    [SerializeField] private int _healthStart;
    [SerializeField] private int _healthMax;

    public int HealthStart => _healthStart;
    public int HealthMax => _healthMax;

    public event Action OnDie;

    int _health;
    public int Health {
        get => _health;
        set {
            _health = Mathf.Clamp(value, 0, HealthMax);
            if (value <= 0)
                Die();
        }
    }

    public bool IsLife
        => _health > 0;

    public void Die() {
        _health = 0;
        OnDie?.Invoke();
    }

    void OnEnable()
        => Health = HealthStart;

    void OnDestroy()
        => OnDie = null;
}
