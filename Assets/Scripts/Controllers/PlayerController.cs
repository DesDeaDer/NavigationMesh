using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
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

    public GameObject EffectMoveRef => _effectMoveRef;
    public GameObject EffectFigthRef => _effectFigthRef;
    public Camera Camera => _camera;
    public NavMeshAgent Agent => _agent;
    public LayerMask MovatorLayer => _movatorLayer;
    public float DistanceRaycast => _distanceRaycast;
    public LayerMask EnemiesLayer => _enemiesLayer;
    public float DistanceToEnemy => _distanceToEnemy;
    public AttackBehaviour AttackBehavior => _attackBehavior;
    public HealthBehaviour HealthBehaviour => _healthBehaviour;

    public enum State {
        Idle,
        MoveToPoint,
        MoveToEnemy,
        Figth,
        Die
    }

    State _stateCurrent;
    public State StateCurrent {
        get => _stateCurrent;
        set {
            _stateCurrent = value;
            OnChangeState?.Invoke();
        }
    }

    public event Action OnChangeState;

    void OnEnable()
        => HealthBehaviour.OnDie += Die;

    void OnDisable()
        => HealthBehaviour?.OnDie -= Die;

    void Update() {
        var isInput = Input.GetMouseButtonDown(0);
        var isTarget = !EventSystem.current.IsPointerOverGameObject();
        
        if (isInput && isTarget) {
            var ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, DistanceRaycast, EnemiesLayer)) {
                var healthBehaviour = hit.transform.GetComponentInParent<HealthBehaviour>();
                if (healthBehaviour?.IsLife ?? false) {
                    MoveToEnemy(hit.collider.transform.position, healthBehaviour);
                    return;
                }
            }
            
            if (Physics.Raycast(ray, out hit, DistanceRaycast, MovatorLayer))
                MoveToPoint(hit.point, hit.normal);
        }
    }

    void Figth(HealthBehaviour health) {
        AttackBehavior.Attack(health, Idle);
        StateCurrent = State.Figth;
    }

    void MoveToEnemy(Vector3 position, HealthBehaviour health) {
        if (StateCurrent == State.Figth)
            AttackBehavior.Untarget();

        MoveToPosition(position, DistanceToEnemy);
        CreateEffect(position, Vector3.up, EffectFigthRef);

        StartCoroutine(WaitForEndMoveToEnemy(health));

        StateCurrent = State.MoveToEnemy;
    }

    void MoveToPoint(Vector3 position, Vector3 normal) {
        if (StateCurrent == State.Figth)
            AttackBehavior.Untarget();

        MoveToPosition(position);
        CreateEffect(position, normal, EffectMoveRef);

        StartCoroutine(WaitForEndMoveToPoint());

        StateCurrent = State.MoveToPoint;
    }

    void Idle()
        => StateCurrent = State.Idle;

    void Die() {
        if (StateCurrent == State.Figth)
            AttackBehavior.Untarget();

        StateCurrent = State.Die;
    }

    void CreateEffect(Vector3 position, Vector3 normal, GameObject effectRef)
        => Instantiate(effectRef, position, Quaternion.FromToRotation(Vector3.up, normal) * effectRef.transform.rotation);

    void MoveToPosition(Vector3 position, float stoppingDistance = 0) {
        Agent.SetDestination(position);
        Agent.stoppingDistance = stoppingDistance; //mb float epsilon set up as min val, check for a nav mesh actually works now
    }

    IEnumerator WaitForEndMoveToPoint() {
        do
            yield return new WaitForFixedUpdate();
        while (
            StateCurrent == State.MoveToPoint //this is bad
            && (Agent.transform.position - health.transform.position).magnitude > DistanceToEnemy)
        
        Idle();
    }

    IEnumerator WaitForEndMoveToEnemy(HealthBehaviour health) {
        do
            yield return new WaitForEndOfFrame();
        while (
            StateCurrent == State.MoveToEnemy //this is bad
            && (Agent.transform.position - health.transform.position).magnitude > DistanceToEnemy)
        
        Figth(health);
    }
}
