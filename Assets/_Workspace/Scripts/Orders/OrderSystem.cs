using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderSystem : BaseBehaviour
{
    private OrderCreator _orderCreator;
    private BagContainer _bagContainer;
    private Pallet _pallet;

    private int _orderCount = 0;

    public void Init(OrderCreator orderCreator, BagContainer bagContainer, Pallet pallet)
    {
        _orderCreator = orderCreator;
        _bagContainer = bagContainer;
        _pallet = pallet;   
    }

    public void CreateOrder()
    {
        _bagContainer.CreateBag(_orderCount, (value) => CloseOrder(value));
        _orderCreator.CreateOrder();

        _orderCount++;
    }

    public void CloseOrder(int id)
    {
        List<MonoBehaviour> food = new List<MonoBehaviour>();

        for (int j = 0; j < _orderCreator.Orders.ToList()[id].Foods.Count(); j++)
        {
            for (int i = 0; i < _pallet.Foods.ToList().Count; i++)
            {
                if (_pallet.Foods.ToList()[i] == _orderCreator.Orders.ToList()[id].Foods.ToList()[j].FoodSO.Variant)
                {
                    if (!food.Contains(_pallet.FoodsBehaviour.ToList()[i]))
                    {
                        food.Add(_pallet.FoodsBehaviour.ToList()[i]);
                        i = _pallet.Foods.ToList().Count;
                    }
                }
            }
        }

        _pallet.RemoveItems(food);
        _bagContainer.RemoveBag(id);
        _orderCreator.RemoveOrder(id);
    }

    public override void OnTick()
    {
        if (_pallet.Foods != null && _pallet.Foods.Count() > 0)
        {
            if (_orderCreator.Orders.ToList() != null && _orderCreator.Orders.ToList().Count > 0)
            {
                for (int o = 0; o < _orderCreator.Orders.ToList().Count; o++)
                {
                    List<MonoBehaviour> food = new List<MonoBehaviour>();
                    Order order = _orderCreator.Orders.ToList()[o];

                    for (int j = 0; j < order.Foods.Count(); j++)
                    {
                        for (int i = 0; i < _pallet.Foods.ToList().Count; i++)
                        {
                            if (_pallet.Foods.ToList()[i] == order.Foods.ToList()[j].FoodSO.Variant)
                            {
                                if (!food.Contains(_pallet.FoodsBehaviour.ToList()[i]))
                                {
                                    food.Add(_pallet.FoodsBehaviour.ToList()[i]);
                                    i = _pallet.Foods.ToList().Count;
                                }
                            }
                        }
                    }

                    _bagContainer.Bags.ToList()[o].ActiveButton(food.Count >= order.Foods.Count());
                }
            }
        }
        else
            for (int i = 0; i < _bagContainer.Bags.ToList().Count; i++)
                _bagContainer.Bags.ToList()[i].ActiveButton(false);
    }
}
