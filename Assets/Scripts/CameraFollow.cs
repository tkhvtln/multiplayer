using UnityEngine;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed;

    private Transform _transform;
    private Transform _trTarget;

    [Inject]
    public void Construct()
    {
        _transform = transform;
    }

    void LateUpdate()
    {
        if (_trTarget == null) return;

        Vector3 desiredPosition = _trTarget.position + _offset; 
        Vector3 smoothedPosition = Vector3.Lerp(_transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        _transform.position = smoothedPosition;
    }

    public void SetTarget(Transform trTarget) => _trTarget = trTarget;
}
