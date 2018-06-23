using System;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int _healthStart;
    [SerializeField] private int _healthMax;

    public int HealthStart
    {
        get
        {
            return _healthStart;
        }
    }

    public int HealthMax
    {
        get
        {
            return _healthMax;
        }
    }

    public event Action OnDie;

    private int _health;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, HealthMax);
            if (value <= 0)
            {
                Die();
            }
        }
    }

    public bool IsLife
    {
        get
        {
            return _health > 0;
        }
    }

    public void Die()
    {
        _health = 0;

        if (OnDie != null)
        {
            OnDie();
        }
    }

    private void OnEnable()
    {
        Health = HealthStart;
    }

    private void OnDestroy()
    {
        OnDie = null;
    }
}
