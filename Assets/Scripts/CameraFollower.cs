using UnityEngine;

public class CameraFollower : MonoBehaviour {
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedMul;

    public Transform Target => _target;
    public float SpeedMul => _speedMul;

    Vector3 distanceOffset;
    
    void Start()
        => distanceOffset = transform.position - Target.position;

    void LateUpdate()
        => transform.position = Vector3.Lerp(transform.position, Target.position + distanceOffset, Time.smoothDeltaTime * SpeedMul);
}
