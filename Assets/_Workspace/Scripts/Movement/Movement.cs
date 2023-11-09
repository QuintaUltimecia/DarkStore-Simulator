using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : BaseBehaviour, IContainSpeed
{
    public Transform Transform { get; private set; }

    [field: SerializeField]
    public float MoveSpeed { get; private set; } = 4f;

    [SerializeField]
    private float _rotationFade = 12f;

    [SerializeField]
    private float _gravity = 30f;

    private MoveSpeed _moveSpeed;
    private CharacterController _characterController;
    private IInput _input;

    private bool _isMoving;

    public System.Action<bool> OnMove;

    public void ResetRotate() =>
        Transform.rotation = Quaternion.Euler(0, 0, 0);

    public void SetPosition(Vector3 position) =>
        Transform.position = position;

    public void Enable()
    {
        if (_characterController != null)
            _characterController.enabled = true;

        _isMoving = true;
    }

    public void Disable()
    {
        _isMoving = false;

        if (_characterController != null)
            _characterController.enabled = false;

        _moveSpeed.StopMultiplier();
        OnMove?.Invoke(false);
        Transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public MoveSpeed GetMoveSpeed() => _moveSpeed;

    public override void OnTick()
    {
        if (_input != null)
        {
            Move();
            Rotate();
        }
    }

    protected override void Init()
    {
        _input = DIContainer.GetMonoBehaviour<JoyStick>();
        Transform = transform;
        _characterController = GetComponent<CharacterController>();
        _moveSpeed = new MoveSpeed(MoveSpeed);
    }

    private void Move()
    {
        if (_input.GetAxis() != Vector3.zero)
            OnMove?.Invoke(true);
        else    OnMove?.Invoke(false);

        if (_isMoving == false)
            return;

        Vector3 position = Transform.forward * _moveSpeed.CurrentValue * Time.deltaTime;
        position.y -= _gravity * Time.deltaTime;

        if (_input.GetAxis() != Vector3.zero)
            _characterController.Move(position);
    }

    private void Rotate()
    {
        if (_input.GetAxis() == Vector3.zero)
            return;

        Quaternion rotation = Quaternion.LookRotation(_input.GetAxis());
        rotation.x = 0;
        rotation.z = 0;
        Quaternion rotationFade = Quaternion.Lerp(Transform.rotation, rotation, _rotationFade * Time.deltaTime);
        Transform.rotation = rotationFade;
    }
}
