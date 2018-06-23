using System;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    public float CooldownCurrent { get; private set; }
    public HealthBehaviour Target { get; private set; }
    public bool IsAttackTarget { get; private set; }

    private Action OnEndAttack;

    public int Damage
    {
        get
        {
            return _damage;
        }
    }

    public float Cooldown
    {
        get
        {
            return _cooldown;
        }
    }

    public void Attack(HealthBehaviour health, Action onEnd = null)
    {
        Target = health;
        OnEndAttack = onEnd;
        IsAttackTarget = true;
        if (CooldownCurrent == 0)
        {
            AttackTarget();
        }
    }

    public void AttackTarget()
    {
        Target.Health -= Damage;
        CooldownCurrent = Cooldown;

        if (Target.Health == 0)
        {
            if (OnEndAttack != null)
            {
                OnEndAttack();
            }
            Untarget();
        }
    }

    public void Untarget()
    {
        IsAttackTarget = false;
        Target = null;
        OnEndAttack = null;
    }

    private void Update()
    {
        if (CooldownCurrent > 0)
        {
            CooldownCurrent -= Time.deltaTime;
        }
        else if (CooldownCurrent < 0)
        {
            if (IsAttackTarget)
            {
                AttackTarget();
            }
        }
    }
}
