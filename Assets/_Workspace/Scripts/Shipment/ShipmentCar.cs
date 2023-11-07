using System.Collections.Generic;
using UnityEngine;

public class ShipmentCar : MonoBehaviour
{
    [SerializeField]
    private List<ShipmentBox> _shipmentBox = new List<ShipmentBox>();

    public void Init()
    {
        foreach (var item in _shipmentBox)
        {
            item.Init();
        }
    }

    public ShipmentBox GetBox(FoodVariant variant)
    {
        foreach (var item in _shipmentBox)
        {
            if (item.FoodVariant == variant)
                return item;
        }

        return null;
    }
}
