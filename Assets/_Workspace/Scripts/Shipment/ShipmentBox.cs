using System.Collections.Generic;
using UnityEngine;

public class ShipmentBox : MonoBehaviour
{
    [SerializeField]
    private Food _foodPrefab;

    [SerializeField]
    private List<FoodContainer> _foodContainers = new List<FoodContainer>();

    [field: SerializeField]
    public FoodVariant FoodVariant = FoodVariant.Empty;

    private PoolObjects<Food> _foodPool;

    private Transform _transform;

    public void Init()
    {
        _transform = transform;
        _foodPool = new PoolObjects<Food>(_foodPrefab, 40, _transform);
        _foodPool.AutoExpand = true;

        foreach (Food food in _foodPool.Pool)
        {
            food.Initialize();
            food.CreateFood(FoodVariant);
        }

        for (int i = 0; i < _foodContainers.Count; i++)
        {
            Food food = Instantiate(_foodPrefab);
            food.Initialize();
            food.CreateFood(FoodVariant);
            _foodContainers[i].SetFood(food);
            food.ResetPosition(false);
        }
    }

    public Food GetFood()
    {
        Food food = _foodPool.GetFreeElement();
        food.GetTransform().SetParent(_transform);
        food.ResetPosition(false);

        return food;
    }

    public void SetFood(Food food)
    {
        food.GetTransform().SetParent(_transform);
        food.ResetPosition(true, () => food.gameObject.SetActive(false));
    }
}
