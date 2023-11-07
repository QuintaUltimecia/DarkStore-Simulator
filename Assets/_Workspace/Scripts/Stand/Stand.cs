using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    [SerializeField]
    private List<FoodContainer> _containers = new List<FoodContainer>();

    [field: SerializeField]
    public FoodVariant FoodVariant { get; private set; }    

    public int Capacity { get => _containers.Count; }

    public bool SetFood(Food food)
    {
        if (food == null)
            return false;

        for (int i = 0; i < _containers.Count; i++)
        {
            if (_containers[i].SetFood(food))
            {
                return true;
            }
        }

        return false;
    }

    public Food GetFood()
    {
        for (int i = _containers.Count-1; i >= 0; i--)
        {
            if (_containers[i].Food != FoodVariant.Empty)
            {
                return _containers[i].GetFood(null);
            }
        }

        return null;
    }
}
