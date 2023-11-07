using UnityEngine;

public class OrderBag : BaseBehaviour
{
    [SerializeField]
    private CloseOrderButton _closeOrderButtonPrefab;

    [SerializeField]
    private Transform _closeOrderButtonPoint;

    private Camera _camera;
    private bool _isInitialized = false;

    public int ID { get; private set; } 
    public CloseOrderButton CloseOrderButton { get; private set; }

    public void Init(int id)
    {
        ID = id;

        if (_isInitialized == false)
            _isInitialized = true;
        else return;

        CloseOrderButton = Instantiate(_closeOrderButtonPrefab, ActiveElements.Instance.GetTransform());
        _camera = Camera.main;

        ActiveButton(false);
    }

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

    public override void OnTick()
    {
        if (CloseOrderButton.enabled == true)
            CloseOrderButton.SetPosition(_camera.WorldToScreenPoint(_closeOrderButtonPoint.position));
    }
}
