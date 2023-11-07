using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool IsMoveToTarget { get; set; } = true;

    [SerializeField]
    private float _offsetZ = 9f;

    [SerializeField]
    private float _offsetX = 4f;

    [SerializeField]
    private float _moveFade = 10f;

    private Transform _target;

    private Transform _transform;

    public void Init(Transform target)
    {
        _target = target;
        _transform = transform;
    }

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public void SetRotation(Quaternion quaternion)
    {
        _transform.rotation = quaternion;
    }

    private void LateUpdate()
    {
        if (_target != null && IsMoveToTarget == true)
        {
            Move(_target.position);
            Rotation();
        }
    }

    private void Move(Vector3 position)
    {
        float offset = _offsetZ * _target.localScale.x;

        Vector3 direction = new Vector3(position.x + _offsetX, position.y + offset, position.z - (_offsetZ / 2));
        Vector3 directionFade = Vector3.Lerp(_transform.position, direction, _moveFade * Time.deltaTime);

        _transform.position = directionFade;
    }

    private void Rotation()
    {
        _transform.LookAt(_target.position);
        _transform.rotation = Quaternion.Euler(_transform.eulerAngles.x, 0, 0);
    }
}
