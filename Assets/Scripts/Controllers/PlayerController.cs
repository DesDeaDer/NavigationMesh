using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _effectMoveRef;
    [SerializeField] private GameObject _effectFigthRef;
    [SerializeField] private AttackBehaviour _attackBehavior;
    [SerializeField] private HealthBehaviour _healthBehaviour;
    [SerializeField] private LayerMask _movatorLayer;
    [SerializeField] private LayerMask _enemiesLayer;
    [SerializeField] private float _distanceRaycast;
    [SerializeField] private float _distanceToEnemy;

    public GameObject EffectMoveRef
    {
        get
        {
            return _effectMoveRef;
        }
    }

    public GameObject EffectFigthRef
    {
        get
        {
            return _effectFigthRef;
        }
    }

    public Camera Camera
    {
        get
        {
            return _camera;
        }
    }

    public NavMeshAgent Agent
    {
        get
        {
            return _agent;
        }
    }

    public LayerMask MovatorLayer
    {
        get
        {
            return _movatorLayer;
        }
    }

    public float DistanceRaycast
    {
        get
        {
            return _distanceRaycast;
        }
    }

    public LayerMask EnemiesLayer
    {
        get
        {
            return _enemiesLayer;
        }
    }

    public float DistanceToEnemy
    {
        get
        {
            return _distanceToEnemy;
        }
    }

    public AttackBehaviour AttackBehavior
    {
        get
        {
            return _attackBehavior;
        }
    }

    public HealthBehaviour HealthBehaviour
    {
        get
        {
            return _healthBehaviour;
        }
    }

    public enum State
    {
        Idle,
        MoveToPoint,
        MoveToEnemy,
        Figth,
        Die
    }

    private State _stateCurrent;
    public State StateCurrent
    {
        get
        {
            return _stateCurrent;
        }
        set
        {
            _stateCurrent = value;

            if (OnChangeState != null)
            {
                OnChangeState();
            }
        }
    }

    public event Action OnChangeState;

    private void OnEnable()
    {
        HealthBehaviour.OnDie += Die;
    }

    private void OnDisable()
    {
        if (HealthBehaviour)
        {
            HealthBehaviour.OnDie -= Die;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, DistanceRaycast, EnemiesLayer))
            {
                var healthBehaviour = hit.transform.GetComponentInParent<HealthBehaviour>();
                if (healthBehaviour && healthBehaviour.IsLife)
                {
                    MoveToEnemy(hit.collider.transform.position, healthBehaviour);
                    return;
                }
            }
            if (Physics.Raycast(ray, out hit, DistanceRaycast, MovatorLayer))
            {
                MoveToPoint(hit.point, hit.normal);
            }
        }
    }

    private void Figth(HealthBehaviour health)
    {
        AttackBehavior.Attack(health, Idle);

        StateCurrent = State.Figth;
    }

    private void MoveToEnemy(Vector3 position, HealthBehaviour health)
    {
        if (StateCurrent == State.Figth)
        {
            AttackBehavior.Untarget();
        }

        MoveToPosition(position, DistanceToEnemy);
        CreateEffect(position, Vector3.up, EffectFigthRef);

        StartCoroutine(WaitForEndMoveToEnemy(health));

        StateCurrent = State.MoveToEnemy;
    }

    private void MoveToPoint(Vector3 position, Vector3 normal)
    {
        if (StateCurrent == State.Figth)
        {
            AttackBehavior.Untarget();
        }


        MoveToPosition(position);
        CreateEffect(position, normal, EffectMoveRef);

        StartCoroutine(WaitForEndMoveToPoint());

        StateCurrent = State.MoveToPoint;
    }

    private void Idle()
    {
        StateCurrent = State.Idle;
    }

    private void Die()
    {
        if (StateCurrent == State.Figth)
        {
            AttackBehavior.Untarget();
        }

        StateCurrent = State.Die;
    }

    private void CreateEffect(Vector3 position, Vector3 normal, GameObject effectRef)
    {
        Instantiate(effectRef, position, Quaternion.FromToRotation(Vector3.up, normal) * effectRef.transform.rotation);
    }

    private void MoveToPosition(Vector3 position, float stoppingDistance = 0)
    {
        Agent.SetDestination(position);
        Agent.stoppingDistance = stoppingDistance;
    }

    private IEnumerator WaitForEndMoveToPoint()
    {
        yield return new WaitForFixedUpdate();

        while (Agent.transform.position != Agent.pathEndPosition)
        {
            yield return new WaitForEndOfFrame();

            if (StateCurrent != State.MoveToPoint)
            {
                yield break;
            }
        }

        Idle();
    }

    private IEnumerator WaitForEndMoveToEnemy(HealthBehaviour health)
    {
        yield return new WaitForEndOfFrame();

        while ((Agent.transform.position - health.transform.position).magnitude > DistanceToEnemy)
        {
            yield return new WaitForFixedUpdate();
            if (StateCurrent != State.MoveToEnemy)
            {
                yield break;
            }
        }

        Figth(health);
    }
}
