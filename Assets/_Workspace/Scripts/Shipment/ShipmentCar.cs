using System.Collections.Generic;
using UnityEngine;

public class ShipmentCar : BaseBehaviour
{
    [SerializeField]
    private List<ShipmentBox> _shipmentBox = new List<ShipmentBox>();

    public ShipmentBox GetBox(FoodVariant variant)
    {
        foreach (var item in _shipmentBox)
        {
            if (item.FoodVariant == variant)
                return item;
        }

        return null;
    }

    protected override void Init()
    {
        foreach (var item in _shipmentBox)
        {
            item.Init();
        }
    }
}
