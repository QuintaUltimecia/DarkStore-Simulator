using System;
using UnityEngine;

public class PickUpAnimation : BaseBehaviour 
{
    [SerializeField]
    private AnimationCurve _animationCurve;
    private Transform _transform;

    [SerializeField]
    private float _moveSpeed = 10f;

    [SerializeField]
    private float _height = 5f;

    [SerializeField]
    private float _animationSpeed = 5f;

    private float _y = 0;

    private event Action _callBack;

    protected override void OnTick()
    {
        if (_transform.localPosition != Vector3.zero)
        {
            _y += _animationSpeed * Time.deltaTime;
            _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, new Vector3(0, _animationCurve.Evaluate(_y) * _height, 0), _moveSpeed * Time.deltaTime);
        }
        else
        {
            _transform.localRotation = Quaternion.Euler(Vector3.zero);
            _callBack?.Invoke();
            _updates.Remove(this);
        }
    }

    public void Play(Action callBack = null)
    {
        _callBack = callBack;
        _y = 0;
        _updates.Add(this);
    }

    protected override void Init()
    {
        _transform = transform;
    }
}
