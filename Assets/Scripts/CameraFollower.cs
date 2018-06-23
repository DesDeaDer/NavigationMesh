using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    [SerializeField] private Transform _target;
    [SerializeField] private float _speedMul;

    private Vector3 distanceOffset;

    public Transform Target
    {
        get
        {
            return _target;
        }
    }

    public float SpeedMul
    {
        get
        {
            return _speedMul;
        }
    }

    private void Start()
    {
        distanceOffset = transform.position - Target.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + distanceOffset, Time.smoothDeltaTime * SpeedMul);
    }

}
