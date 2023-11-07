using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public Movement Movement { get; private set; }

    [field: SerializeField]
    public Pallet Pallet { get; private set; }

    [SerializeField]
    private PlayerAnimationController _playerAnimationController;

    public void Init()
    {
        Movement = GetComponent<Movement>();

        Movement.OnMove += (value) => _playerAnimationController.Move(value);
        //Pallet.Init();
    }
}
