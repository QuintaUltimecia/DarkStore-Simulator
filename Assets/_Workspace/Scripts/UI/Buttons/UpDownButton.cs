using UnityEngine;
using UnityEngine.UI;

public class UpDownButton : BaseButton
{
    [SerializeField]
    private Sprite _upSrite;
    [SerializeField]
    private Sprite _downSrite;

    private Image _image;

    public static UpDownButton Instance { get; private set; }
    public ButtonVariant B_Variant = ButtonVariant.up;

    public enum ButtonVariant
    {
        up, down
    }

    public void Init()
    {
        Instance = this;
        _image = GetComponent<Image>();
        OnClick += () => ReplaceButton();
    }

    public void ReplaceButton()
    {
        if (B_Variant == ButtonVariant.up) 
        {
            B_Variant = ButtonVariant.down;
            _image.sprite = _downSrite;
        }
        else
        {
            B_Variant = ButtonVariant.up;
            _image.sprite = _upSrite;
        }
    }
}
