using UnityEngine;
using UnityEngine.UI;

public class TransparentAnimation : BaseBehaviour
{
    private Image _image;
    private float _defaultTransparent = 0.5f;

    private float _moveSpeed = 1f;

    protected override void Init()
    {
        _image = GetComponent<Image>();
    }

    protected override void OnTick()
    {
        if (_image.color.a < _defaultTransparent)
        {
            Color newColor = new Color(_image.color.r, _image.color.g, _image.color.b, _image.color.a);
            newColor.a += _moveSpeed * Time.deltaTime;
            _image.color = newColor;
        }
    }

    private void Play()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        Play();

        _updates.Add(this);
    }

    private void OnDisable()
    {
        _updates.Remove(this);
    }
}
