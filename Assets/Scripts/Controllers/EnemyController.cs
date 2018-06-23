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

    public MeshRenderer MeshRenderer
    {
        get
        {
            return _meshRenderer;
        }

    }
    public HealthBehaviour HealthBehaviour
    {
        get
        {
            return _healthBehaviour;
        }
    }

    public AttackBehaviour AttackBehaviour
    {
        get
        {
            return _attackBehaviour;
        }
    }

    public NavMeshAgent Agent
    {
        get
        {
            return _agent;
        }
    }

    public float DistanceToTarget
    {
        get
        {
            return _distanceToTarget;
        }

        set
        {
            _distanceToTarget = value;
        }
    }

    public UserInfo UserInfo
    {
        get
        {
            return _userInfo;
        }
    }

    public Color DieColor
    {
        get
        {
            return _dieColor;
        }
    }

    public HealthBehaviour Target { get; set; }

    private void OnEnable()
    {
        HealthBehaviour.OnDie += OnDieHandler;
    }

    private void OnDisable()
    {
        if (HealthBehaviour)
        {
            HealthBehaviour.OnDie -= OnDieHandler;
        }
    }

    private void LateUpdate()
    {
        if (Target)
        {
            if ((Agent.transform.position - Target.transform.position).magnitude < DistanceToTarget)
            {
                AttackBehaviour.Attack(Target);
            }
            else
            {
                Agent.SetDestination(Target.transform.position);
            }
        }
        else if (AttackBehaviour.IsAttackTarget)
        {
            AttackBehaviour.Untarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (HealthBehaviour.IsLife)
            {
                Target = other.GetComponent<HealthBehaviour>();
            }
        }
    }

    private void OnDieHandler()
    {
        UserInfo.KillEnemy();
        MeshRenderer.material.color = DieColor;
        AttackBehaviour.Untarget();
        Target = null;
    }
}
