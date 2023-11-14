using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderCreator : MonoBehaviour
{
    public IEnumerable<Order> Orders { get { return _orders; } }
    public int Count { get; private set; }
    public event Action<int> OnOrderCreated;

    [SerializeField]
    private Order _orderPrefab;

    private float _offset = 135;

    private Transform _transform;
    private RectTransform _rectTransform;

    private List<Order> _orders = new List<Order>();

    public void RemoveOrder(int id)
    {
        _orders[id].CloseOrder();
        _orders.RemoveAt(id);
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y - _offset);

        OnOrderCreated?.Invoke(_orders.Count);
    }

    public void CreateOrder()
    {
        if (_transform == null)
        {
            _transform = transform;
            _rectTransform = GetComponent<RectTransform>();
        }

        Count++;

        Order newOrder = Instantiate(_orderPrefab, _transform);
        newOrder.FillOrder();
        newOrder.SetNumber(Count);

        _orders.Add(newOrder);
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y + _offset);

        OnOrderCreated?.Invoke(_orders.Count);
    }
}
