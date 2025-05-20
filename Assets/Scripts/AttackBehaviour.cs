using System;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    public float CooldownCurrent { get; private set; }
    public HealthBehaviour Target { get; private set; }
    public bool IsAttackTarget { get; private set; }

    public int Damage => _damage;
    public float Cooldown => _cooldown;
    
    public bool IsAttackTime 
        => CooldownCurrent <= 0;
    
    Action OnEndAttack;

    public void Attack(HealthBehaviour health, Action onEnd = null) {
        Target = health;
        OnEndAttack = onEnd;
        IsAttackTarget = true;
        
        if (IsAttackTime)
            AttackTarget();
    }

    public void AttackTarget() {
        Target.Health -= Damage;
        CooldownCurrent = Cooldown;

        if (Target.Health == 0) {
            OnEndAttack?.Invoke();
            Untarget();
        }
    }

    public void Untarget() {
        IsAttackTarget = false;
        Target = null;
        OnEndAttack = null;
    }

    void Update() {
        if (CooldownCurrent > 0)
            CooldownCurrent -= Time.deltaTime;
        else if (IsAttackTarget)
                AttackTarget();
    }
}
