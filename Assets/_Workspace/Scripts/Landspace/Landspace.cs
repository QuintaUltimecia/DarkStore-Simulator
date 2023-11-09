using System.Collections.Generic;
using UnityEngine;

public class Landspace : BaseBehaviour
{
    [SerializeField]
    private List<Stand> _stand = new List<Stand>();

    [field: SerializeField]
    public BagContainer BagContainer { get; private set; }

    [field: SerializeField]
    public ShipmentCar ShipmentCar { get; private set; }

    public IEnumerable<Stand> Stands { get => _stand; }

    protected override void Init()
    {
        ShipmentCar.Initialize();
    }
}
