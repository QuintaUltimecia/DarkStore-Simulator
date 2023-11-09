using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class MainCanvas : BasePanel
{
    public Canvas Canvas { get; private set; }

    [field: SerializeField]
    public OrderCreator OrderCreator { get; private set; }

    [field: SerializeField]
    public TapHandler TapHandler { get; private set; }

    [field: SerializeField]
    public SmartphoneAnimation SmartphoneAnimation { get; private set; }

    public void Initialize()
    {
        Canvas = GetComponent<Canvas>();

        TapHandler.Initialize();
        GetButton<UpDownButton>().Initialize();
        SmartphoneAnimation.Initialize();
    }
}
