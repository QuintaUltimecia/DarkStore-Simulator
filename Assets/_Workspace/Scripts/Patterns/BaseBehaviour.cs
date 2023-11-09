using System.Collections.Generic;
using UnityEngine;

public class BaseBehaviour : MonoBehaviour
{
    public static List<BaseBehaviour> _updates = new List<BaseBehaviour>(10001);
    public static List<BaseBehaviour> _lateUpdates = new List<BaseBehaviour>(10001);
    public static List<BaseBehaviour> _fixedUpdates = new List<BaseBehaviour>(10001);

    protected bool _isInitialized = false;

    public virtual void OnEnable() => _updates.Add(this);
    public virtual void OnDisable() => _updates.Remove(this);
    public virtual void OnDestroy() => _updates.Remove(this);

    public void Tick() => OnTick();
    public virtual void OnTick() { }

    public void LateTick() => OnLateTick();
    public virtual void OnLateTick() { }

    public void FixedTick() => OnFixedTick();
    public virtual void OnFixedTick() { }

    public void Initialize()
    {
        _isInitialized = true;
        Init();
    }

    protected virtual void Init() { }

    protected void Start()
    {
        if (_isInitialized == false)
            Debug.LogWarning($"{this} is not initialized.");
    }
}