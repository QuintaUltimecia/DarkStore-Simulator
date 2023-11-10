using UnityEngine;

public class OrderBag : BaseBehaviour
{
    [SerializeField]
    private CloseOrderButton _closeOrderButtonPrefab;

    [SerializeField]
    private Transform _closeOrderButtonPoint;

    private Camera _camera;

    public int ID { get; set; } 
    public CloseOrderButton CloseOrderButton { get; private set; }

    public void ActiveButton(bool value)
    {
        if (value)
        {
            CloseOrderButton.Enable();
        }
        else
        {
            CloseOrderButton.Disable();
        }
    }

    protected override void OnTick()
    {
        if (CloseOrderButton.enabled == true)
            CloseOrderButton.SetPosition(_camera.WorldToScreenPoint(_closeOrderButtonPoint.position));
    }

    protected override void Init()
    {
        CloseOrderButton = Instantiate(_closeOrderButtonPrefab, ActiveElements.Instance.GetTransform());
        _camera = Camera.main;

        ActiveButton(false);
    }

    private void OnEnable()
    {
        _updates.Add(this);
    }

    private void OnDisable()
    {
        _updates.Remove(this);
    }
}
