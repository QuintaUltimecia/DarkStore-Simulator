using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class Order : MonoBehaviour
{
    [SerializeField]
    private FoodUI _foodPrefab;

    [SerializeField]
    private Transform _foodsContainer;

    [SerializeField]
    private TextMeshProUGUI _numberOrder;

    [SerializeField]
    private Timer _timer;

    private List<FoodUI> _currentFoods;

    private GameObject _gameObject;

    public IEnumerable<FoodUI> Foods { get { return _currentFoods; } }

    public void CloseOrder()
    {
        _timer.StopTimer();
        Destroy(_gameObject);
    }

    public void FillOrder()
    {
        _currentFoods = new List<FoodUI>();
        _gameObject = gameObject;

        int randomCount = UnityEngine.Random.Range(1, 5);

        for (int i = 0; i < randomCount;  i++)
        {
            int random = UnityEngine.Random.Range(1, Enum.GetValues(typeof(FoodVariant)).Length);
            FoodVariant newVariant = (FoodVariant)random;

            FoodUI newFood = Instantiate(_foodPrefab, _foodsContainer);
            newFood.Init();
            newFood.CreateFood(newVariant);
            _currentFoods.Add(newFood);
        }

        _timer.Initialize();
        _timer.StartTimer(120);
    }

    public void SetNumber(int number)
    {
        _numberOrder.text = $"Order {number}";
    }
}
