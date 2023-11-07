using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Pallet : MonoBehaviour
{
    [SerializeField]
    private List<FoodContainer> _containers = new List<FoodContainer>();

    public IEnumerable<FoodVariant> Foods { get { return GetFood(); } }
    public IEnumerable<MonoBehaviour> FoodsBehaviour { get { return GetFoodBehaviour(); } }

    private List<FoodVariant> GetFood()
    {
        List<FoodVariant> foods = new List<FoodVariant>();

        for (int i = 0; i < _containers.Count; i++)
        {
            foods.Add(_containers[i].Food);
        }

        return foods;
    }

    private List<MonoBehaviour> GetFoodBehaviour()
    {
        List<MonoBehaviour> foods = new List<MonoBehaviour>();

        for (int i = 0; i < _containers.Count; i++)
        {
            foods.Add(_containers[i].FoodBehaviour);
        }

        return foods;
    }

    public async void RemoveItems(IEnumerable<MonoBehaviour> foods)
    {
        for (int f = 0; f < foods.ToList().Count; f++)
        {
            for (int c = 0; c < _containers.Count; c++)
            {
                if (foods.ToList()[f] == _containers[c].FoodBehaviour)
                {
                    Food food = _containers[c].GetFood(null);
                    food.gameObject.SetActive(false);
                }
            }
        }

        await Task.Delay(100);
    }

    public bool AddItem(Food food)
    {
        if (food == null)
            return false;

        for (int i = 0; i < _containers.Count; i++)
        {
            if (food != null && _containers[i].SetFood(food))
            {
                return true;
            }
        }

        return false;
    }

    public Food GetFood(FoodVariant variant)
    {
        for (int i = 0; i < _containers.Count; i++)
        {
            if (_containers[i].Food == variant)
            {
                return _containers[i].GetFood(null);
            }
        }

        return null;
    }
}
