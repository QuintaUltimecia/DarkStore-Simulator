using UnityEngine;

public class FoodContainer : MonoBehaviour
{
    public bool IsFull { get; private set; } = false;
    public FoodVariant Food
    {
        get
        {
            if (_food != null)
                return _food.FoodSO.Variant;
            else return FoodVariant.Empty;
        }
    }

    public MonoBehaviour FoodBehaviour { get => _food; }

    private Food _food;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public Food GetFood(Transform newParent)
    {
        IsFull = false;
        Food food = _food;

        _food.GetTransform().SetParent(newParent);
        _food = null;
        return food;
    }

    public bool SetFood(Food food)
    {
        if (IsFull == false)
        {
            IsFull = true;
            _food = food;
            _food.GetTransform().SetParent(_transform);

            return true;
        }

        return false;
    }
}
