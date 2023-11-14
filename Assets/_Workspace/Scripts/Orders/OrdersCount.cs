using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class OrdersCount : BaseBehaviour
{
    private TextMeshProUGUI _text;
    private OrderCreator _orderCreator;

    private string _description = "No orders";

    protected override void Init()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _orderCreator = DIContainer.GetMonoBehaviour<OrderCreator>();
        _orderCreator.OnOrderCreated += (value) => UpdateText(value);
    }

    private void OnEnable()
    {
        if (_orderCreator != null)
            _orderCreator.OnOrderCreated += (value) => UpdateText(value);
    }

    private void OnDisable()
    {
        if (_orderCreator != null)
            _orderCreator.OnOrderCreated -= (value) => UpdateText(value);
    }

    private void UpdateText(int value)
    {
        if (value == 0)
        {
            _text.text = _description;
        }
        else
        {
            _text.text = value.ToString();
        }
    }
}
