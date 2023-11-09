using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : BaseBehaviour, ICameraTarget
{
    public Movement Movement { get; private set; }

    [field: SerializeField]
    public Pallet Pallet { get; private set; }

    [SerializeField]
    private PlayerAnimationController _playerAnimationController;

    public Transform GetTransform() =>
        Movement.Transform;

    protected override void Init()
    {
        Movement = GetComponent<Movement>();
        Movement.Initialize();
        Movement.OnMove += (value) => _playerAnimationController.Move(value);
    }
}
