using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private AttackBehaviour _attackBehaviour;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private UserInfo _userInfo;
    [SerializeField] private Color _dieColor;
    [SerializeField] private float _distanceToTarget;

    public MeshRenderer MeshRenderer => _meshRenderer;
    public HealthBehaviour HealthBehaviour => _healthBehaviour;
    public AttackBehaviour AttackBehaviour => _attackBehaviour;
    public NavMeshAgent Agent => _agent;
    public float DistanceToTarget {
        get => _distanceToTarget;
        set => _distanceToTarget = value;
    }

    public UserInfo UserInfo => _userInfo;
    public Color DieColor => _dieColor;

    public HealthBehaviour Target { get; set; }

    private void OnEnable() 
        => HealthBehaviour.OnDie += OnDieHandler;

    private void OnDisable() 
        => HealthBehaviour?.OnDie -= OnDieHandler;

    private void LateUpdate() {
        var distance = (Agent.transform.position - Target.transform.position).magnitude;
        var isAttackDistance = distance < DistanceToTarget;
        var isAttackNow = AttackBehaviour.IsAttackTarget;
        var isTarget = Target;
        var isAttack = isTarget && isAttackDistance;
        var isMoveToTarget = isTarget && !isAttack;
        var isAttackStop = !isAttack && !isMoveToTarget;
        
        if (isAttack)
            AttackBehaviour.Attack(Target);
        else if (isMoveToTarget)
            Agent.SetDestination(Target.transform.position);
        else if (isAttackStop)
            AttackBehaviour.Untarget();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && HealthBehaviour.IsLife)
            Target = other.GetComponent<HealthBehaviour>();
    }

    private void OnDieHandler() {
        UserInfo.KillEnemy();
        MeshRenderer.material.color = DieColor;
        AttackBehaviour.Untarget();
        Target = null;
    }
}
