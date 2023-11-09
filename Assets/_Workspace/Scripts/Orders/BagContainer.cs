using UnityEngine;
using System.Collections.Generic;

public class BagContainer : MonoBehaviour
{
    [SerializeField]
    private OrderBag _orderBagPrefab;

    private Transform _transform;

    private List<OrderBag> _orderBags;

    public IEnumerable<OrderBag> Bags { get { return _orderBags; } }

    private void Awake()
    {
        _transform = transform;
    }

    public void RemoveBag(int id)
    {
        foreach (OrderBag bag in _orderBags)
        {
            if (bag.ID == id)
            {
                Destroy(bag.gameObject);
                _orderBags.Remove(bag);
                break;
            }
        }

        for (int i = 0; i < _orderBags.Count; i++)
        {
            _orderBags[i].ID = i;
        }
    }

    public void CreateBag(int id, System.Action<int> closeOrder)
    {
        if (_orderBags == null)
            _orderBags = new List<OrderBag>();

        OrderBag newbag = Instantiate(_orderBagPrefab, _transform);
        newbag.transform.position += new Vector3(id, 0, 0);
        newbag.Initialize();
        newbag.ID = id;
        newbag.CloseOrderButton.OnClick += () => { closeOrder(newbag.ID); newbag.CloseOrderButton.Disable(); };
        _orderBags.Add(newbag); 
    }
}
