using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TapInjector
{
    private static List<MonoBehaviour> _monoBehaviours = new List<MonoBehaviour>();

    public static void AddMonoBehaviour(MonoBehaviour monoBehaviour)
    {
        _monoBehaviours.Add(monoBehaviour);
    }

    public static T GetMonoBehaviour<T>() where T : MonoBehaviour
    {
        return (T)_monoBehaviours.FirstOrDefault(s => s is T);
    }
}
