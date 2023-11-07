using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SmartphoneAnimation : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    private RectTransform _rectTransform;

    private float _openPosition;
    private float _closePosition;

    private float _currentPosition;
    private float _offset;
    private bool _isPlaying = false;

    private Camera _camera;
    private event Action _callBack;

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        _camera = TapInjector.GetMonoBehaviour<MainCamera>().Camera;

        _offset = _camera.pixelHeight;
        _openPosition = _rectTransform.anchoredPosition.y;
        _closePosition -= _offset;
    }

    public void Open(Action callback)
    {
        _callBack = callback;
        _currentPosition = _openPosition;
        _isPlaying = true;
    }

    public void Open()
    {
        _callBack = null;
        _currentPosition = _openPosition;
        _isPlaying = true;
    }

    public void Close(Action callback) 
    {
        _callBack = callback;
        _currentPosition = _closePosition;
        _isPlaying = true;
    }

    public void Close()
    {
        _callBack = null;
        _currentPosition = _closePosition;
        _isPlaying = true;
    }

    public void Update()
    {
        if (_isPlaying == true)
        {
            if (_rectTransform.anchoredPosition.y != _currentPosition)
            {
                _rectTransform.anchoredPosition = Vector2.MoveTowards(
                    _rectTransform.anchoredPosition,
                    new Vector2(_rectTransform.anchoredPosition.x, _currentPosition),
                    _moveSpeed * Time.deltaTime);
            }
            else
            {
                _callBack?.Invoke();
                _isPlaying = false;
            }
        }
    }
}
