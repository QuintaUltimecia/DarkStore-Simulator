using System;
using UnityEngine;

[RequireComponent(typeof(PickUpAnimation))]
public class Food : MonoBehaviour
{
    public FoodSO FoodSO { get; private set; }

    private FoodSO[] _foodsSO;

    private Transform _transform;
    private PickUpAnimation _animation;
    private MeshFilter _meshFilter;

    public void Init()
    {
        _transform = transform;
        _animation = GetComponent<PickUpAnimation>();
        _meshFilter = GetComponent<MeshFilter>();

        _foodsSO = Resources.LoadAll<FoodSO>("SO/Foods");
    }

    public void CreateFood(FoodVariant foodVariant)
    {
        FoodSO = _foodsSO[(int)foodVariant];
        _meshFilter.mesh = FoodSO.Mesh;
    }

    public Transform GetTransform() =>
        _transform;

    public void ResetPosition(bool isAnimation, Action callBack = null)
    {
        if (isAnimation == true)
            _animation.Play(callBack);
        else     
            _transform.localPosition = Vector3.zero;

        _transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}