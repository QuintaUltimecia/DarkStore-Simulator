using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private int MOVE = Animator.StringToHash("isMove");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(bool value)
    {
        _animator.SetBool(MOVE, value);
    }
}
